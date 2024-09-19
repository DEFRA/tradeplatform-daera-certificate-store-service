// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Logic.Services.Interfaces;
using Defra.Trade.API.CertificatesStore.V2.Dtos;
using Defra.Trade.API.CertificatesStore.V2.Services.Interfaces;
using Models = Defra.Trade.API.CertificatesStore.Logic.Models;

namespace Defra.Trade.API.CertificatesStore.V2.Services;

public class GeneralCertificateEnrichmentService(
    IMapper mapper,
    IIdcomsGeneralCertificateEnrichmentService idcomsGeneralCertificateEnrichmentService) : IGeneralCertificateEnrichmentService
{
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    private readonly IIdcomsGeneralCertificateEnrichmentService _idcomsGeneralCertificateEnrichmentService = idcomsGeneralCertificateEnrichmentService
        ?? throw new ArgumentNullException(nameof(idcomsGeneralCertificateEnrichmentService));

    public async Task<IdcomsGeneralCertificateEnrichment> SaveEnrichmentAsync(
        IdcomsGeneralCertificateEnrichment generalCertificateEnrichment,
        CancellationToken cancellationToken = default)
    {
        var toSave = _mapper.Map<Models.IdcomsGeneralCertificateEnrichment>(generalCertificateEnrichment);
        var result = await _idcomsGeneralCertificateEnrichmentService.SaveEnrichmentAsync(toSave, cancellationToken);

        var response = _mapper.Map<IdcomsGeneralCertificateEnrichment>(result);

        return response;
    }
}
