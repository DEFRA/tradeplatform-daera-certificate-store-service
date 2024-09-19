// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Database.Models;

namespace Defra.Trade.API.CertificatesStore.Repository.Interfaces;

public interface IEnrichmentStoreRepository
{
    Task<EnrichmentData> CreateAsync(
        string gcId,
        EnrichmentData enrichmentData,
        CancellationToken cancellationToken = default);
}
