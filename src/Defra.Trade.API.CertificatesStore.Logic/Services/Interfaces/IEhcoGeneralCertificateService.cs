// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Logic.Models;

namespace Defra.Trade.API.CertificatesStore.Logic.Services.Interfaces;

public interface IEhcoGeneralCertificateService
{
    Task<EhcoGeneralCertificateApplication> SaveAsync(
        EhcoGeneralCertificateApplication generalCertificate,
        CancellationToken cancellationToken = default);
}