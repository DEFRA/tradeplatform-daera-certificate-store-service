// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Diagnostics.CodeAnalysis;
using Defra.Trade.API.CertificatesStore.Infrastructure;
using Defra.Trade.API.CertificatesStore.Logic.Extensions;
using Defra.Trade.API.CertificatesStore.Settings;
using Defra.Trade.Common.Api.Infrastructure;
using Defra.Trade.Common.AppConfig;

namespace Defra.Trade.API.CertificatesStore;

/// <summary>
/// Application entry point.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Tested as part of system integration tests.")]
public sealed class Program
{
    private Program()
    {
    }

    /// <summary>
    /// Runs the application.
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.ConfigureTradeAppConfiguration(opt =>
        {
            opt.UseKeyVaultSecrets = true;
            opt.RefreshKeys.Add($"{CertificateStoreOptions.SectionName}:Sentinel");
            opt.Select<CertificateStoreOptions>(CertificateStoreOptions.SectionName);
        });

        builder.Services.AddTradeApi(builder.Configuration);
        builder.Services.AddServiceRegistrations(builder.Configuration);

        var app = builder.Build();

        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        logger.LogStartup(
            app.Environment.EnvironmentName,
            app.Environment.ApplicationName,
            app.Environment.ContentRootPath);

        app.UseTradeApp(app.Environment);

        app.Run();
    }
}
