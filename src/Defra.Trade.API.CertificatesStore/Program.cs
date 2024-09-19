// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Diagnostics.CodeAnalysis;
using Defra.Trade.API.CertificatesStore.Settings;
using Defra.Trade.Common.AppConfig;
using Microsoft.Extensions.Hosting;

namespace Defra.Trade.API.CertificatesStore;

/// <summary>
/// Application program file.
/// </summary>
public static class Program
{
    /// <summary>
    /// Application main class.
    /// </summary>
    /// <param name="args">Args</param>
    [ExcludeFromCodeCoverage(Justification = "Tested as part of system integration tests.")]
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(config =>
            {
                config.ConfigureTradeAppConfiguration(opt =>
                {
                    opt.UseKeyVaultSecrets = true;
                    opt.RefreshKeys.Add($"{CertificateStoreOptions.SectionName}:Sentinel");
                    opt.Select<CertificateStoreOptions>(CertificateStoreOptions.SectionName);
                });
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}