// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Linq;
using AutoMapper;
using Defra.Trade.API.CertificatesStore.Database.Models;
using Defra.Trade.API.CertificatesStore.Logic.Extensions;
using Defra.Trade.API.CertificatesStore.Logic.Helpers;
using Defra.Trade.API.CertificatesStore.Logic.Models;
using Defra.Trade.API.CertificatesStore.Logic.Services.Interfaces;
using Defra.Trade.API.CertificatesStore.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace Defra.Trade.API.CertificatesStore.Logic.Services;

public class EhcoGeneralCertificateService(
    ICertificatesStoreRepository certificateStoreRepository,
    IMapper mapper,
    ILogger<EhcoGeneralCertificateService> logger,
    IGeneralCertificateDocumentRepository gcAttachmentRepository) : IEhcoGeneralCertificateService
{
    private const string SystemName = "EHCO";
    private const int DefaultSchemaVersion = 2;
    private readonly ICertificatesStoreRepository _certificateStoreRepository = certificateStoreRepository ?? throw new ArgumentNullException(nameof(certificateStoreRepository));
    private readonly IGeneralCertificateDocumentRepository _gcAttachmentRepository = gcAttachmentRepository ?? throw new ArgumentNullException(nameof(gcAttachmentRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<EhcoGeneralCertificateService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<EhcoGeneralCertificateApplication> SaveAsync(
        EhcoGeneralCertificateApplication generalCertificate,
        CancellationToken cancellationToken = default)
    {
        _logger.GcReceived(generalCertificate?.ExchangedDocument?.Id);

        string creatorId = Guid.Empty.ToString();
        var timeNow = DateTimeOffset.UtcNow;

        var certificate = _mapper.Map<GeneralCertificate>(generalCertificate);

        certificate.SchemaVersion = DefaultSchemaVersion;
        certificate.CreatedBy = creatorId;
        certificate.CreatedOn = timeNow;
        certificate.CreatedSystem = SystemName;
        certificate.LastUpdatedBy = creatorId;
        certificate.LastUpdatedOn = timeNow;
        certificate.LastUpdatedSystem = SystemName;

        var existingGc = await _certificateStoreRepository.GetAsync(certificate.GeneralCertificateId, cancellationToken);

        if (existingGc != null)
        {
            _logger.GcDuplicate(certificate.GeneralCertificateId);
            return null;
        }

        await _certificateStoreRepository.CreateAsync(certificate, cancellationToken);
        _logger.GcSaved(certificate.GeneralCertificateId);

        if (generalCertificate!.ExchangedDocument!.PackingListFileLocation == null)
        {
            _logger.GcMissingPackingListFileLocation(certificate.GeneralCertificateId);
            return generalCertificate;
        }
        await SaveGcAttachmentAsync(
            certificate,
            generalCertificate!.ExchangedDocument!.PackingListFileLocation,
            creatorId,
            timeNow,
            DaeraPayloadConstants.PackingListPdfTypeCode,
            cancellationToken);

        if (!string.IsNullOrWhiteSpace(generalCertificate!.ExchangedDocument!.CertificatePDFLocation))
        {
            await SaveGcAttachmentAsync(
                certificate,
                generalCertificate!.ExchangedDocument!.CertificatePDFLocation,
                creatorId,
                timeNow,
                DaeraPayloadConstants.GcPdfTypeCode,
                cancellationToken);
        }
        return generalCertificate;
    }

    private async Task SaveGcAttachmentAsync(
        GeneralCertificate generalCertificate,
        string packingFileLocation,
        string creatorId,
        DateTimeOffset createdOn,
        int typeCode,
        CancellationToken cancellationToken)
    {
        var packingFileLocationUri = new Uri(packingFileLocation);
        var gcAttachment = new GeneralCertificateDocument
        {
            GeneralCertificate = generalCertificate,
            Url = packingFileLocationUri.ToString(),
            FileName = packingFileLocationUri.Segments.LastOrDefault(),
            ListAgencyId = DaeraPayloadConstants.ListAgencyId,
            TypeCode = typeCode,
            DocumentId = packingFileLocationUri.Segments.LastOrDefault(),
            CreatedBy = creatorId,
            CreatedOn = createdOn,
            CreatedSystem = SystemName,
            LastUpdatedBy = creatorId,
            LastUpdatedOn = createdOn,
            LastUpdatedSystem = SystemName
        };

        await _gcAttachmentRepository.CreateAsync(gcAttachment, cancellationToken);
    }
}
