// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Logic.Models;

namespace Defra.Trade.API.CertificatesStore.Logic.Services.Interfaces;

public interface IIdcomsGeneralCertificateEnrichmentService
{
    Task<IdcomsGeneralCertificateEnrichment> SaveEnrichmentAsync(
        IdcomsGeneralCertificateEnrichment generalCertificateEnrichment,
        CancellationToken cancellationToken = default);
}