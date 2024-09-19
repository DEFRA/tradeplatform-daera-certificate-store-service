// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Database.Models;
using Defra.Trade.API.CertificatesStore.IntegrationTests.Infrastructure;
using Defra.Trade.API.CertificatesStore.IntegrationTests.Utilities;
using Defra.Trade.API.CertificatesStore.Logic.Helpers;
using Defra.Trade.API.CertificatesStore.V2.Dtos;
using Defra.Trade.Common.Api.Dtos;

namespace Defra.Trade.API.CertificatesStore.IntegrationTests.V1.Controllers.EhcoGeneralCertificateApplicationControllerTests;

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
    public async Task Save_ValidNewGcV2Schema_NoContent()
    {
        // Arrange
        var exchangedDocumentPayload = _fixture.Build<ExchangedDocument>()
            .With(x => x.PackingListFileLocation, "https://www.org.uk/mocked-file.json")
            .With(x => x.CertificatePDFLocation, "https://www.org.uk/mocked-gcpdf-file.json")
            .Create();
        var payload = _fixture.Build<EhcoGeneralCertificateApplication>()
            .With(x => x.ExchangedDocument, exchangedDocumentPayload)
            .Create();

        _webApplicationFactory.CertificatesStoreRepository
            .Setup(r =>
                r.CreateAsync(It.Is<GeneralCertificate>(c => c.GeneralCertificateId == payload.ExchangedDocument.Id), It.IsAny<CancellationToken>()));

        _webApplicationFactory.GeneralCertificateDocumentRepository
            .Setup(r =>
                r.CreateAsync(It.Is<GeneralCertificateDocument>(c =>
                    c.Url == payload.ExchangedDocument.PackingListFileLocation &&
                    c.TypeCode == DaeraPayloadConstants.PackingListPdfTypeCode), It.IsAny<CancellationToken>()));

        _webApplicationFactory.GeneralCertificateDocumentRepository
            .Setup(r =>
                r.CreateAsync(It.Is<GeneralCertificateDocument>(c =>
                    c.Url == payload.ExchangedDocument.CertificatePDFLocation &&
                    c.TypeCode == DaeraPayloadConstants.GcPdfTypeCode), It.IsAny<CancellationToken>()));

        var client = _webApplicationFactory.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync("ehco-gc-application?sv=2", payload);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        _webApplicationFactory.CertificatesStoreRepository
            .Verify(r =>
                r.CreateAsync(
                    It.Is<GeneralCertificate>(c =>
                        c.GeneralCertificateId == payload.ExchangedDocument.Id
                        && c.Id == Guid.Empty
                        && c.IsActive
                        && !string.IsNullOrWhiteSpace(c.Data)
                        && c.CreatedSystem == "EHCO"
                        && c.SchemaVersion == 2),
                    It.IsAny<CancellationToken>()),
                Times.Once);

        _webApplicationFactory.GeneralCertificateDocumentRepository
            .Verify(r =>
                    r.CreateAsync(
                        It.Is<GeneralCertificateDocument>(c =>
                            c.Url == payload.ExchangedDocument.PackingListFileLocation
                            && c.GeneralCertificate!.GeneralCertificateId == payload.ExchangedDocument.Id),
                        It.IsAny<CancellationToken>()),
                Times.Once);

        _webApplicationFactory.GeneralCertificateDocumentRepository
            .Verify(r =>
                    r.CreateAsync(
                        It.Is<GeneralCertificateDocument>(c =>
                            c.Url == payload.ExchangedDocument.CertificatePDFLocation
                            && c.GeneralCertificate!.GeneralCertificateId == payload.ExchangedDocument.Id),
                        It.IsAny<CancellationToken>()),
                Times.Once);
    }

    [Fact]
    public async Task Save_ValidNewGcWithOutPackingListFileLocation_WithSchema_2_NoContent()
    {
        // Arrange
        var exchangedDocumentPayload = _fixture.Build<ExchangedDocument>()
            .With(x => x.PackingListFileLocation, "https://www.org.com/packingpdf")
            .With(x => x.CertificatePDFLocation, "https://www.org.com/gcpdf")
            .Create();
        var payload = _fixture.Build<EhcoGeneralCertificateApplication>()
            .With(x => x.ExchangedDocument, exchangedDocumentPayload)
            .Create();

        _webApplicationFactory.CertificatesStoreRepository
            .Setup(r =>
                r.CreateAsync(It.Is<GeneralCertificate>(c => c.GeneralCertificateId == payload.ExchangedDocument.Id), It.IsAny<CancellationToken>()));

        _webApplicationFactory.GeneralCertificateDocumentRepository
            .Setup(r =>
                r.CreateAsync(It.Is<GeneralCertificateDocument>(c => c.Url == payload.ExchangedDocument.PackingListFileLocation), It.IsAny<CancellationToken>()));

        _webApplicationFactory.GeneralCertificateDocumentRepository
            .Setup(r =>
                r.CreateAsync(It.Is<GeneralCertificateDocument>(c => c.Url == payload.ExchangedDocument.CertificatePDFLocation), It.IsAny<CancellationToken>()));

        var client = _webApplicationFactory.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync("ehco-gc-application?sv=2", payload);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        _webApplicationFactory.CertificatesStoreRepository
            .Verify(r =>
                r.CreateAsync(
                    It.Is<GeneralCertificate>(c =>
                        c.GeneralCertificateId == payload.ExchangedDocument.Id
                        && c.Id == Guid.Empty
                        && c.IsActive
                        && !string.IsNullOrWhiteSpace(c.Data)
                        && c.CreatedSystem == "EHCO"
                        && c.SchemaVersion == 2),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        _webApplicationFactory.GeneralCertificateDocumentRepository
            .Verify(r =>
                    r.CreateAsync(
                        It.Is<GeneralCertificateDocument>(c =>
                            c.Url == payload.ExchangedDocument.PackingListFileLocation
                            && c.GeneralCertificate!.GeneralCertificateId == payload.ExchangedDocument.Id),
                        It.IsAny<CancellationToken>()),
                Times.Once);

        _webApplicationFactory.GeneralCertificateDocumentRepository
            .Verify(r =>
                    r.CreateAsync(
                        It.Is<GeneralCertificateDocument>(c =>
                            c.Url == payload.ExchangedDocument.CertificatePDFLocation
                            && c.GeneralCertificate!.GeneralCertificateId == payload.ExchangedDocument.Id),
                        It.IsAny<CancellationToken>()),
                Times.Once);
    }

    [Fact]
    public async Task Save_ValidNewGcWithOutPackingListFileLocation_BadRequest()
    {
        // Arrange
        var exchangedDocumentPayload = _fixture.Build<ExchangedDocument>()
            .Without(x => x.PackingListFileLocation)
            .Without(x => x.CertificatePDFLocation)
            .Create();
        var payload = _fixture.Build<EhcoGeneralCertificateApplication>()
            .With(x => x.ExchangedDocument, exchangedDocumentPayload)
            .Create();

        _webApplicationFactory.CertificatesStoreRepository
            .Setup(r =>
                r.CreateAsync(It.Is<GeneralCertificate>(c => c.GeneralCertificateId == payload.ExchangedDocument.Id), It.IsAny<CancellationToken>()));

        _webApplicationFactory.GeneralCertificateDocumentRepository
            .Setup(r =>
                r.CreateAsync(It.Is<GeneralCertificateDocument>(c => c.Url == payload.ExchangedDocument.PackingListFileLocation), It.IsAny<CancellationToken>()));

        _webApplicationFactory.GeneralCertificateDocumentRepository
            .Setup(r =>
                r.CreateAsync(It.Is<GeneralCertificateDocument>(c => c.Url == payload.ExchangedDocument.CertificatePDFLocation), It.IsAny<CancellationToken>()));

        var client = _webApplicationFactory.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync("ehco-gc-application", payload);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Save_NoGcId_BadRequest()
    {
        // Arrange
        var payload = _fixture
            .Build<EhcoGeneralCertificateApplication>()
            .With(x => x.ExchangedDocument, _fixture
                .Build<ExchangedDocument>()
                .With(d => d.PackingListFileLocation, "https://www.mocked.org/testname.png")
                .With(d => d.CertificatePDFLocation, "https://www.mocked.org/testname.png")
                .With(d => d.Id, string.Empty)
                .Create())
            .Create();

        var client = _webApplicationFactory.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync("ehco-gc-application", payload);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var content = await response.Content.ReadAsAsync<CommonProblemDetails>();

        content.Should().NotBeNull();

        if (content == null)
            return;

        content.Status.Should().Be((int)HttpStatusCode.BadRequest);
        content.Title.Should().NotBeNullOrWhiteSpace();
        content.Type.Should().Match(t => Uri.IsWellFormedUriString(t, UriKind.RelativeOrAbsolute));

        content.Errors.Should()
            .HaveCount(1)
            .And.ContainKey("exchangedDocument.id");
    }

    [Fact]
    public async Task Save_WhenSchemaIsV2_NoGcPdf_ShouldReturn_BadRequest()
    {
        // Arrange
        var payload = _fixture
            .Build<EhcoGeneralCertificateApplication>()
            .With(x => x.ExchangedDocument, _fixture
                .Build<ExchangedDocument>()
                .With(d => d.PackingListFileLocation, "https://www.mocked.org/testname.png")
                .Without(d => d.CertificatePDFLocation)
                .Create())
            .Create();

        var client = _webApplicationFactory.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync("ehco-gc-application?sv=2", payload);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Save_WhenSchemaIsV2_InvalidGcPdf_ShouldReturn_BadRequest()
    {
        // Arrange
        var payload = _fixture
            .Build<EhcoGeneralCertificateApplication>()
            .With(x => x.ExchangedDocument, _fixture
                .Build<ExchangedDocument>()
                .With(d => d.PackingListFileLocation, "https://www.mocked.org/testname.png")
                .With(d => d.CertificatePDFLocation, "mocked")
                .Create())
            .Create();

        var client = _webApplicationFactory.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync("ehco-gc-application?sv=2", payload);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var content = await response.Content.ReadAsAsync<CommonProblemDetails>();

        content.Should().NotBeNull();

        if (content == null)
            return;

        content.Status.Should().Be((int)HttpStatusCode.BadRequest);
        content.Title.Should().NotBeNullOrWhiteSpace();
        content.Type.Should().Match(t => Uri.IsWellFormedUriString(t, UriKind.RelativeOrAbsolute));

        content.Errors.Should()
            .HaveCount(1)
            .And.ContainKey("exchangedDocument.certificatePDFLocation");
    }

    [Fact]
    public async Task Save_WhenSchemaIsV2_InvalidGcPdfWithHttp_ShouldReturn_BadRequest()
    {
        // Arrange
        var payload = _fixture
            .Build<EhcoGeneralCertificateApplication>()
            .With(x => x.ExchangedDocument, _fixture
                .Build<ExchangedDocument>()
                .With(d => d.PackingListFileLocation, "http://www.mocked.org/testname.png")
                .With(d => d.CertificatePDFLocation, "mocked")
                .Create())
            .Create();

        var client = _webApplicationFactory.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync("ehco-gc-application?sv=2", payload);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var content = await response.Content.ReadAsAsync<CommonProblemDetails>();

        content.Should().NotBeNull();

        if (content == null)
            return;

        content.Status.Should().Be((int)HttpStatusCode.BadRequest);
        content.Title.Should().NotBeNullOrWhiteSpace();
        content.Type.Should().Match(t => Uri.IsWellFormedUriString(t, UriKind.RelativeOrAbsolute));

        content.Errors.Should()
            .HaveCount(1)
            .And.ContainKey("exchangedDocument.certificatePDFLocation");
    }
}
