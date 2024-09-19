// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Logic.Services.Interfaces;
using Defra.Trade.API.CertificatesStore.V2.Dtos;
using Defra.Trade.API.CertificatesStore.V2.Services.Interfaces;
using Models = Defra.Trade.API.CertificatesStore.Logic.Models;

namespace Defra.Trade.API.CertificatesStore.V2.Services;

public class GeneralCertificatesService(
    IMapper mapper,
    IEhcoGeneralCertificateService ehcoGeneralCertificateService) : IGeneralCertificatesService
{
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    private readonly IEhcoGeneralCertificateService _ehcoGeneralCertificateService = ehcoGeneralCertificateService
        ?? throw new ArgumentNullException(nameof(ehcoGeneralCertificateService));

    public async Task<EhcoGeneralCertificateApplication> SaveGeneralCertificateAsync(
        EhcoGeneralCertificateApplication generalCertificate,
        CancellationToken cancellationToken = default)
    {
        var toSave = _mapper.Map<Models.EhcoGeneralCertificateApplication>(generalCertificate);

        var result = await _ehcoGeneralCertificateService.SaveAsync(toSave, cancellationToken);

        var response = _mapper.Map<EhcoGeneralCertificateApplication>(result);

        return response;
    }
}
