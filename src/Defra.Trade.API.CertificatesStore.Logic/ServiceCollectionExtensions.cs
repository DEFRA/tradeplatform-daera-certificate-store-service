// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Logic.Services;
using Defra.Trade.API.CertificatesStore.Logic.Services.Interfaces;
using Defra.Trade.API.CertificatesStore.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Defra.Trade.API.CertificatesStore.Logic;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepository(configuration);

        services
            .AddScoped<IEhcoGeneralCertificateService, EhcoGeneralCertificateService>()
            .AddScoped<IIdcomsGeneralCertificateEnrichmentService, IdcomsGeneralCertificateEnrichmentService>();
    }
}