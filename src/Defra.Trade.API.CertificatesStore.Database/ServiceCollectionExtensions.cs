// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Database.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Defra.Trade.API.CertificatesStore.Database;

public static class ServiceCollectionExtensions
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CertificatesStoreDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("sql_db_ef")).UseLazyLoadingProxies());

        services.AddHealthChecks()
            .AddDbContextCheck<CertificatesStoreDbContext>(
                name: "DatabaseHealthCheck",
                customTestQuery: (db, cancel) =>
                    Task.FromResult(db.Database.SqlQueryRaw<int>("SELECT 1 AS TestColumn").Any())
            );
    }
}