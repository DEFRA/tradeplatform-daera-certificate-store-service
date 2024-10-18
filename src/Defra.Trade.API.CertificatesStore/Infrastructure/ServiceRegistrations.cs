// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Database.Context;
using Defra.Trade.API.CertificatesStore.Logic;
using Defra.Trade.API.CertificatesStore.Logic.Mappers;
using Defra.Trade.API.CertificatesStore.Settings;
using Defra.Trade.API.CertificatesStore.V2.Services;
using Defra.Trade.API.CertificatesStore.V2.Services.Interfaces;

namespace Defra.Trade.API.CertificatesStore.Infrastructure;

/// <summary>
/// Service registration class.
/// </summary>
public static class ServiceRegistrations
{
    /// <summary>
    /// Extension method for service registrations.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="configuration">Application configuration.</param>
    /// <returns></returns>
    public static IServiceCollection AddServiceRegistrations(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddCertificatesStoreHealthChecks()
            .AddValidatorsFromAssemblyContaining<Startup>(lifetime: ServiceLifetime.Transient)
            .AddAutoMapper(typeof(Startup), typeof(GeneralCertificateSaveMapper))
            .AddV1Registrations()
            .Configure<CertificateStoreOptions>(configuration.GetSection(CertificateStoreOptions.SectionName))
            .AddServices(configuration);

        return services;
    }

    private static IServiceCollection AddCertificatesStoreHealthChecks(this IServiceCollection services)
    {
        services
            .AddHealthChecks()
            .AddDbContextCheck<CertificatesStoreDbContext>();

        return services;
    }

    private static IServiceCollection AddV1Registrations(this IServiceCollection services)
    {
        services
            .AddScoped<IGeneralCertificatesService, GeneralCertificatesService>()
            .AddScoped<IGeneralCertificateEnrichmentService, GeneralCertificateEnrichmentService>();

        return services;
    }
}
