// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Database.Extensions;
using Defra.Trade.API.CertificatesStore.Database.Models;
using Defra.Trade.API.CertificatesStore.Database.Services.Interfaces;

namespace Defra.Trade.API.CertificatesStore.Database.Services;

/// <inheritdoc />
public class DbHealthCheckService<TDbContext>(TDbContext context) : IDbHealthCheckService
    where TDbContext : DbContext
{
    private readonly TDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc />
    public async Task<DatabaseInfo> GetContextInfo()
    {
        return await _context.GetInfo();
    }
}
