// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Database;
using Defra.Trade.API.CertificatesStore.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Defra.Trade.API.CertificatesStore.Repository;

public static class ServiceCollectionExtensions
{
    public static void AddRepository(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);

        services.AddScoped<ICertificatesStoreRepository, CertificatesStoreRepository>();
        services.AddScoped<IEnrichmentStoreRepository, EnrichmentStoreRepository>();
        services.AddScoped<IGeneralCertificateDocumentRepository, GeneralCertificateDocumentRepository>();
    }
}