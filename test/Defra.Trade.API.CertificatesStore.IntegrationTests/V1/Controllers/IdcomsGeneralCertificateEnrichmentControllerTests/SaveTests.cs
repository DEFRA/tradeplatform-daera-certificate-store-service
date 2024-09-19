// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Database.Models;
using Defra.Trade.API.CertificatesStore.IntegrationTests.Infrastructure;
using Defra.Trade.API.CertificatesStore.IntegrationTests.Utilities;
using Defra.Trade.API.CertificatesStore.V2.Dtos;
using Defra.Trade.Common.Api.Dtos;

namespace Defra.Trade.API.CertificatesStore.IntegrationTests.V1.Controllers.IdcomsGeneralCertificateEnrichmentControllerTests;

public class SaveTests : IClassFixture<CertificatesStoreApplicationFactory<Startup>>
{
    private readonly CertificatesStoreApplicationFactory<Startup> _webApplicationFactory;
    private readonly Fixture _fixture;

    public SaveTests(CertificatesStoreApplicationFactory<Startup> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;

        _fixture = new Fixture();
        _fixture.Customize<GeneralCertificate>(opt => opt.Without(gc => gc.EnrichmentData));
    }

    [Fact]
    public async Task Save_ValidNewGcEnrichment_NoContent()
    {
        // Arrange
        var payload = _fixture.Create<IdcomsGeneralCertificateEnrichment>();
        var gcPayload = _fixture.Create<GeneralCertificate>();
        var enrichmentData = _fixture.Create<EnrichmentData>();

        _webApplicationFactory.CertificatesStoreRepository
            .Setup(r =>
            r.GetAsync(It.Is<string>(c => c == payload.GcId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(gcPayload);

        _webApplicationFactory.EnrichmentStoreRepository
            .Setup(r =>
            r.CreateAsync(It.Is<string>(c => c == payload.GcId), It.IsAny<EnrichmentData>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(enrichmentData);

        var client = _webApplicationFactory.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync("idcoms-gc-enrichment", payload);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        _webApplicationFactory.EnrichmentStoreRepository
        .Verify(r =>
            r.CreateAsync(
                It.Is<string>(c => c == payload.GcId),
                It.Is<EnrichmentData>(c => c.GeneralCertificate == gcPayload
                    && c.Id == Guid.Empty
                    && c.IsActive
                    && !string.IsNullOrWhiteSpace(c.Data)
                    && c.CreatedSystem == "IDCOMS"
                    && c.SchemaVersion == 1),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Save_NewGcEnrichment_GcExistsAndGcEnrichmentExists_NoContent()
    {
        // Arrange
        var payload = _fixture.Create<IdcomsGeneralCertificateEnrichment>();
        var enrichmentData = _fixture.Create<EnrichmentData>();
        var gcPayload = _fixture.Build<GeneralCertificate>()
            .With(gc => gc.EnrichmentData, enrichmentData)
            .Create();

        _webApplicationFactory.CertificatesStoreRepository
           .Setup(r =>
           r.GetAsync(It.Is<string>(c => c == payload.GcId), It.IsAny<CancellationToken>()))
           .ReturnsAsync(gcPayload);

        var client = _webApplicationFactory.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync("idcoms-gc-enrichment", payload);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        _webApplicationFactory.CertificatesStoreRepository
        .Verify(r =>
            r.GetAsync(
                It.Is<string>(c => c == payload.GcId),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _webApplicationFactory.EnrichmentStoreRepository
            .Verify(r =>
                r.CreateAsync(
                    It.Is<string>(i => i == payload.GcId),
                    It.Is<EnrichmentData>(d => d == enrichmentData),
                    It.IsAny<CancellationToken>()),
                Times.Never);
    }

    [Fact]
    public async Task Save_InvalidNewGcEnrichment_BadRequest()
    {
        // Arrange
        var payload = _fixture.Build<IdcomsGeneralCertificateEnrichment>()
            .With(gc => gc.GcId, (string?)null)
            .Create();

        var client = _webApplicationFactory.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync("idcoms-gc-enrichment", payload);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var content = await response.Content.ReadAsAsync<CommonProblemDetails>();

        content.Should().NotBeNull();
        content?.Status.Should().Be((int)HttpStatusCode.BadRequest);
        content?.Errors.Should().HaveCount(1);
        content?.Errors.Should().ContainKey("gcId");
        content?.Errors.First().Value.FirstOrDefault().Should().Be("The GcId field cannot be empty.");
    }

    [Fact]
    public async Task Save_NewGcEnrichment_GcDoesNotExist_BadRequest()
    {
        // Arrange
        var payload = _fixture.Create<IdcomsGeneralCertificateEnrichment>();
        var enrichmentData = _fixture.Create<EnrichmentData>();

        _webApplicationFactory.CertificatesStoreRepository
            .Setup(r =>
                r.GetAsync(It.Is<string>(c => c == payload.GcId), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GeneralCertificate?)null);

        var client = _webApplicationFactory.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync("idcoms-gc-enrichment", payload);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var content = await response.Content.ReadAsAsync<CommonProblemDetails>();

        content.Should().NotBeNull();
        content?.Status.Should().Be((int)HttpStatusCode.BadRequest);
        content?.Errors.Should().HaveCount(1);
        content?.Errors.Should().ContainKey("generalCertificateNotFoundException");
        content?.Errors.First().Value.FirstOrDefault().Should().Be($"No EHCO GC Application {payload.GcId} found in cache store.");

        _webApplicationFactory.EnrichmentStoreRepository
            .Verify(r =>
                r.CreateAsync(
                    It.Is<string>(i => i == payload.GcId),
                    It.Is<EnrichmentData>(d => d == enrichmentData),
                    It.IsAny<CancellationToken>()),
                Times.Never);
    }
}
