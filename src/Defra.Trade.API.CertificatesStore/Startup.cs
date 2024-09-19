// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Infrastructure;
using Defra.Trade.API.CertificatesStore.Logic.Extensions;
using Defra.Trade.Common.Api.Infrastructure;

namespace Defra.Trade.API.CertificatesStore;

/// <summary>
/// Startup class.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="Startup"/> class.
/// </remarks>
/// <param name="configuration">Application Config.</param>
public class Startup(IConfiguration configuration)
{
    private IConfiguration Configuration { get; } = configuration;

    /// <summary>
    /// Method to configure application startup.
    /// </summary>
    /// <param name="app">Application builder.</param>
    /// <param name="env">Web environment</param>
    /// <param name="logger">Application logger</param>
    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
    {
        logger.LogStartup(
            env.EnvironmentName,
            env.ApplicationName,
            env.ContentRootPath);

        app.UseTradeApp(env);
    }

    /// <summary>
    /// Config services registrations.
    /// </summary>
    /// <param name="services">Application Service collection.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTradeApi(Configuration);
        services.AddServiceRegistrations(Configuration);
    }
}
