// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Database.Models;
using Defra.Trade.API.CertificatesStore.IntegrationTests.Infrastructure;

namespace Defra.Trade.API.CertificatesStore.IntegrationTests.V1.Controllers.DocumentRetrievalControllerTests;

public class ControllerTests : IClassFixture<CertificatesStoreApplicationFactory<Startup>>
{
    private readonly Fixture _fixture;
    private readonly CertificatesStoreApplicationFactory<Startup> _webApplicationFactory;

    public ControllerTests(CertificatesStoreApplicationFactory<Startup> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
        _fixture = new Fixture();
        _fixture.Customize<GeneralCertificate>(opt => opt.Without(gc => gc.EnrichmentData));
    }

    [Fact]
    public async Task SaveDocumentRetrieved_ExistingId_NoContent()
    {
        // arrange
        var data = _fixture.Create<GeneralCertificateDocument>();
        data.Retrieved = DateTime.UtcNow;

        _webApplicationFactory.GeneralCertificateDocumentRepository
            .Setup(r => r.SaveRetrievalAsync(data.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(data);

        var client = _webApplicationFactory.CreateClient();

        // act
        var response = await client.PutAsync($"document-retrieval/{data.Id}", null);

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        _webApplicationFactory.GeneralCertificateDocumentRepository
            .Verify(x =>
                x.SaveRetrievalAsync(
                    It.Is<Guid>(x => x == data.Id),
                    It.IsAny<CancellationToken>()),
                    Times.Once);
    }

    [Fact]
    public async Task SaveDocumentRetrieved_NonExistentId_Problem()
    {
        // arrange
        var docId = Guid.NewGuid();

        _webApplicationFactory.GeneralCertificateDocumentRepository
            .Setup(r => r.SaveRetrievalAsync(docId, It.IsAny<CancellationToken>()))
            .Throws<KeyNotFoundException>();

        var client = _webApplicationFactory.CreateClient();

        // act
        var response = await client.PutAsync($"document-retrieval/{docId}", null);

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        _webApplicationFactory.GeneralCertificateDocumentRepository
            .Verify(x =>
                x.SaveRetrievalAsync(
                    It.Is<Guid>(x => x == docId),
                    It.IsAny<CancellationToken>()),
                    Times.Once);
    }

    [Fact]
    public async Task GetRetrieval_ExistingIdAndRetrieved_Ok()
    {
        // arrange
        var data = _fixture.Create<GeneralCertificateDocument>();
        data.Retrieved = DateTime.UtcNow;

        _webApplicationFactory.GeneralCertificateDocumentRepository
            .Setup(r => r.GetAsync(data.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(data);

        var client = _webApplicationFactory.CreateClient();

        // act
        var response = await client.GetAsync($"document-retrieval/{data.Id}");

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        string responseContent = await response.Content.ReadAsStringAsync();
        bool result = JsonSerializer.Deserialize<bool>(responseContent);
        result!.Should().BeTrue();

        _webApplicationFactory.GeneralCertificateDocumentRepository
            .Verify(x =>
                x.GetAsync(
                    It.Is<Guid>(x => x == data.Id),
                    It.IsAny<CancellationToken>()),
                    Times.Once);
    }

    [Fact]
    public async Task GetRetrieval_ExistingIdAndNotRetrieved_Ok()
    {
        // arrange
        var data = _fixture.Create<GeneralCertificateDocument>();
        data.Retrieved = null!;

        _webApplicationFactory.GeneralCertificateDocumentRepository
            .Setup(r => r.GetAsync(data.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(data);

        var client = _webApplicationFactory.CreateClient();

        // act
        var response = await client.GetAsync($"document-retrieval/{data.Id}");

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        string responseContent = await response.Content.ReadAsStringAsync();
        bool result = JsonSerializer.Deserialize<bool>(responseContent);
        result!.Should().BeFalse();

        _webApplicationFactory.GeneralCertificateDocumentRepository
            .Verify(x =>
                x.GetAsync(
                    It.Is<Guid>(x => x == data.Id),
                    It.IsAny<CancellationToken>()),
                    Times.Once);
    }

    [Fact]
    public async Task GetRetrieval_NonExistentId_NotFound()
    {
        // arrange
        var docId = Guid.NewGuid();
        GeneralCertificateDocument nullResponse = null!;

        _webApplicationFactory.GeneralCertificateDocumentRepository
            .Setup(r => r.GetAsync(docId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(nullResponse);

        var client = _webApplicationFactory.CreateClient();

        // act
        var response = await client.GetAsync($"document-retrieval/{docId}");

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        _webApplicationFactory.GeneralCertificateDocumentRepository
            .Verify(x =>
                x.GetAsync(
                    It.Is<Guid>(x => x == docId),
                    It.IsAny<CancellationToken>()),
                    Times.Once);
    }
}
