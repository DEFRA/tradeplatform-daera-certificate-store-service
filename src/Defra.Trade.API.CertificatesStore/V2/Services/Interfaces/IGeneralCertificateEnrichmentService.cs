// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.V2.Dtos;

namespace Defra.Trade.API.CertificatesStore.V2.Services.Interfaces;

public interface IGeneralCertificateEnrichmentService
{
    Task<IdcomsGeneralCertificateEnrichment> SaveEnrichmentAsync(
        IdcomsGeneralCertificateEnrichment generalCertificateEnrichment,
        CancellationToken cancellationToken = default);
}
