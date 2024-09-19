// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Database.Context;
using Defra.Trade.API.CertificatesStore.Database.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Defra.Trade.API.CertificatesStore.Repository.Tests;

public class CertificatesStoreRepositoryTests
{
    private readonly CertificatesStoreDbContext _dbContext;
    private readonly CertificatesStoreRepository _sut;
    private readonly string _dbName;

    public CertificatesStoreRepositoryTests()
    {
        _dbName = Guid.NewGuid().ToString();
        _dbContext = CreateContext();
        _sut = new(_dbContext);
    }

    [Fact]
    public async Task CreateAsync_SavesEnrichmentData_ReturnsPayload()
    {
        // arrange
        string gcId = Guid.NewGuid().ToString();
        var data = new GeneralCertificate
        {
            GeneralCertificateId = gcId,
            Id = Guid.NewGuid(),
            Data = Guid.NewGuid().ToString(),
            CreatedBy = Guid.NewGuid().ToString()
        };
        var token = new CancellationTokenSource().Token;

        // act
        var actual = await _sut.CreateAsync(data, token);

        // assert
        actual.Should().Be(data);
        await using var context = CreateContext();
        var generalCertificate = await context.GeneralCertificate.SingleAsync();
        generalCertificate.Should().BeEquivalentTo(data, opt => opt.IgnoringCyclicReferences());
    }

    [Fact]
    public async Task GetAsync_WhenCalled_ReturnsPayload()
    {
        // arrange
        string gcId = Guid.NewGuid().ToString();
        var data = new GeneralCertificate
        {
            GeneralCertificateId = gcId,
            Id = Guid.NewGuid(),
            Data = Guid.NewGuid().ToString(),
            CreatedBy = Guid.NewGuid().ToString()
        };
        var token = new CancellationTokenSource().Token;
        await using (var context = CreateContext())
        {
            context.GeneralCertificate.Add(data);
            await context.SaveChangesAsync(token);
        }

        // act
        var actual = await _sut.GetAsync(gcId, token);

        // assert
        actual.Should().BeEquivalentTo(data);
        await using (var context = CreateContext())
        {
            var generalCertificate = await context.GeneralCertificate.SingleAsync(x => x.GeneralCertificateId == gcId);
            generalCertificate.Should().BeEquivalentTo(data, opt => opt.IgnoringCyclicReferences());
        }
    }

    private CertificatesStoreDbContext CreateContext()
    {
        var builder = new DbContextOptionsBuilder<CertificatesStoreDbContext>()
            .UseInMemoryDatabase(_dbName);
        return new CertificatesStoreDbContext(builder.Options);
    }
}
