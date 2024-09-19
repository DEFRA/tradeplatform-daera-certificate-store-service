// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using AutoFixture;
using Defra.Trade.API.CertificatesStore.Database.Context;
using Defra.Trade.API.CertificatesStore.Database.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Defra.Trade.API.CertificatesStore.Repository.Tests;

public class GeneralCertificateDocumentRepositoryTests
{
    private readonly CertificatesStoreDbContext _dbContext;
    private readonly string _dbName;
    private readonly Fixture _fixture;
    private readonly GeneralCertificateDocumentRepository _sut;

    public GeneralCertificateDocumentRepositoryTests()
    {
        _fixture = new Fixture();
        _dbName = Guid.NewGuid().ToString();
        _dbContext = CreateContext();
        _sut = new GeneralCertificateDocumentRepository(_dbContext);
    }

    [Fact]
    public async Task CreateAsync_SavesGcDocument_ShouldSaveAsExpected()
    {
        // arrange
        BuildMinimumGeneralCertificateDocument(out var generalCertificate, out var gcDocument);
        var token = new CancellationTokenSource().Token;

        // act
        var actual = await _sut.CreateAsync(gcDocument, token);

        // assert
        actual.Should().Be(gcDocument);
        await using var context = CreateContext();
        var gc = await context.GeneralCertificateDocument.Include(e => e.GeneralCertificate).SingleAsync();
        gc.GeneralCertificate.Should().BeEquivalentTo(generalCertificate, opt => opt.IgnoringCyclicReferences());
    }

    [Fact]
    public async Task GetAsync_ReturnsGeneralCertificate()
    {
        // arrange
        BuildMinimumGeneralCertificateDocument(out _, out var gcDocument);
        var ct = CancellationToken.None;
        gcDocument.Retrieved = DateTime.UtcNow;
        await _sut.CreateAsync(gcDocument, ct);

        // act
        var actual = await _sut.GetAsync(gcDocument.Id, ct);

        // assert
        actual.Should().Be(gcDocument);
    }

    [Fact]
    public async Task SaveRetrieval_ReturnsUpdatedGeneralCertificateDocument()
    {
        // arrange
        var ct = CancellationToken.None;
        BuildMinimumGeneralCertificateDocument(out _, out var gcDocument);
        gcDocument.Retrieved = DateTime.UtcNow;
        await _sut.CreateAsync(gcDocument, ct);

        // act
        var result = await _sut.SaveRetrievalAsync(gcDocument.Id, ct);

        // assert
        result.Retrieved.Should().Be(gcDocument.Retrieved);
        result.LastUpdatedSystem.Should().Be("DaeraCerts");
    }

    [Fact]
    public async Task SaveRetrieval_NoResults_ThrowsKeyNotFoundException()
    {
        // arrange
        var ct = CancellationToken.None;
        Guid docId = Guid.NewGuid();

        // act && assert
        var result = await Assert.ThrowsAsync<KeyNotFoundException>(
            async () => await _sut.SaveRetrievalAsync(docId, ct));

        result.Message.Should().Be($"document with ID {docId} not found");
    }

    private void BuildMinimumGeneralCertificateDocument(out GeneralCertificate generalCertificate, out GeneralCertificateDocument gcDocument)
    {
        generalCertificate = _fixture.Build<GeneralCertificate>().Without(x => x.EnrichmentData).Create();
        gcDocument = new GeneralCertificateDocument
        {
            Id = Guid.NewGuid(),
            CreatedBy = Guid.NewGuid().ToString(),
            Url = "mocked",
            GeneralCertificate = generalCertificate
        };
    }

    private CertificatesStoreDbContext CreateContext()
    {
        var builder = new DbContextOptionsBuilder<CertificatesStoreDbContext>()
            .UseInMemoryDatabase(_dbName);
        return new CertificatesStoreDbContext(builder.Options);
    }
}
