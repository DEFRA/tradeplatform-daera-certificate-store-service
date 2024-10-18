// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Defra.Trade.API.CertificatesStore.IntegrationTests.Infrastructure;

public class CertificatesStoreApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    public Mock<ICertificatesStoreRepository> CertificatesStoreRepository { get; set; }
    public Mock<IEnrichmentStoreRepository> EnrichmentStoreRepository { get; set; }
    public Mock<IGeneralCertificateDocumentRepository> GeneralCertificateDocumentRepository { get; set; }

    public CertificatesStoreApplicationFactory()
    {
        ClientOptions.AllowAutoRedirect = false;
        CertificatesStoreRepository = new Mock<ICertificatesStoreRepository>();
        EnrichmentStoreRepository = new Mock<IEnrichmentStoreRepository>();
        GeneralCertificateDocumentRepository = new Mock<IGeneralCertificateDocumentRepository>();
    }

    private string ApiVersion { get; set; } = "1";

    protected override void ConfigureClient(HttpClient client)
    {
        base.ConfigureClient(client);

        client.BaseAddress = new Uri("https://localhost:5001");
        client.DefaultRequestHeaders.Add("x-api-version", ApiVersion);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.UseEnvironment(Environments.Production);

        builder.ConfigureAppConfiguration((context, configBuilder) =>
        {
            if (configBuilder.Sources.FirstOrDefault(cs => cs.GetType().Name == "AzureKeyVaultConfigurationSource") is { } keyVaultSource)
                configBuilder.Sources.Remove(keyVaultSource);

            configBuilder.AddInMemoryCollection(
                new Dictionary<string, string?>
                {
                    ["https_port"] = "",
                    ["OpenApi:UseXmlComments"] = "false",
                    ["CommonError:ExposeErrorDetail"] = "true"
                });
        });

        builder.ConfigureTestServices(services =>
        {
            services.Replace(ServiceDescriptor.Singleton(CertificatesStoreRepository.Object));
            services.Replace(ServiceDescriptor.Singleton(EnrichmentStoreRepository.Object));
            services.Replace(ServiceDescriptor.Singleton(GeneralCertificateDocumentRepository.Object));
        });
    }
}
