// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Database.Context;
using Defra.Trade.API.CertificatesStore.Database.Models;
using Defra.Trade.API.CertificatesStore.Repository.Interfaces;

namespace Defra.Trade.API.CertificatesStore.Repository;

public class EnrichmentStoreRepository : IEnrichmentStoreRepository
{
    private readonly CertificatesStoreDbContext _context;

    public EnrichmentStoreRepository(CertificatesStoreDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _context = context;
    }

    public async Task<EnrichmentData> CreateAsync(string gcId, EnrichmentData enrichmentData, CancellationToken cancellationToken = default)
    {
        _context.EnrichmentData.Add(enrichmentData);
        await _context.SaveChangesAsync(cancellationToken);

        return enrichmentData;
    }
}
