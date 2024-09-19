// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoMapper;
using Defra.Trade.API.CertificatesStore.Database.Models;
using Defra.Trade.API.CertificatesStore.Logic.Exceptions;
using Defra.Trade.API.CertificatesStore.Logic.Extensions;
using Defra.Trade.API.CertificatesStore.Logic.Models;
using Defra.Trade.API.CertificatesStore.Logic.Services.Interfaces;
using Defra.Trade.API.CertificatesStore.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace Defra.Trade.API.CertificatesStore.Logic.Services;

public class IdcomsGeneralCertificateEnrichmentService(
    ICertificatesStoreRepository certificateStoreRepository,
    IEnrichmentStoreRepository enrichmentStoreRepository,
    IMapper mapper,
    ILogger<IdcomsGeneralCertificateEnrichmentService> logger) : IIdcomsGeneralCertificateEnrichmentService
{
    private const string SystemName_Idcoms = "IDCOMS";

    private readonly ICertificatesStoreRepository _certificateStoreRepository = certificateStoreRepository ?? throw new ArgumentNullException(nameof(certificateStoreRepository));
    private readonly IEnrichmentStoreRepository _enrichmentStoreRepository = enrichmentStoreRepository ?? throw new ArgumentNullException(nameof(enrichmentStoreRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<IdcomsGeneralCertificateEnrichmentService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<IdcomsGeneralCertificateEnrichment> SaveEnrichmentAsync(IdcomsGeneralCertificateEnrichment generalCertificateEnrichment, CancellationToken cancellationToken = default)
    {
        _logger.EnrichmentReceived(generalCertificateEnrichment.GcId);

        string creatorId = Guid.Empty.ToString();
        var timeNow = DateTimeOffset.UtcNow;

        var existingGc = await _certificateStoreRepository.GetAsync(generalCertificateEnrichment.GcId, cancellationToken);

        if (existingGc == null)
        {
            _logger.GcMissing(generalCertificateEnrichment.GcId);
            throw new GeneralCertificateNotFoundException($"No EHCO GC Application {generalCertificateEnrichment.GcId} found in cache store.");
        }

        if (existingGc.EnrichmentData != null)
        {
            _logger.EnrichmentDuplicate(generalCertificateEnrichment.GcId);
            return null;
        }

        var enrichment = _mapper.Map<EnrichmentData>(generalCertificateEnrichment);

        enrichment.GeneralCertificate = existingGc;
        enrichment.CreatedBy = creatorId;
        enrichment.CreatedOn = timeNow;
        enrichment.CreatedSystem = SystemName_Idcoms;
        enrichment.LastUpdatedBy = creatorId;
        enrichment.LastUpdatedOn = timeNow;
        enrichment.LastUpdatedSystem = SystemName_Idcoms;

        _ = await _enrichmentStoreRepository.CreateAsync(generalCertificateEnrichment.GcId, enrichment, cancellationToken);

        _logger.EnrichmentSaved(generalCertificateEnrichment.GcId);

        return generalCertificateEnrichment;
    }
}
