// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Database.Context;
using Defra.Trade.API.CertificatesStore.Database.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Defra.Trade.API.CertificatesStore.Repository.Tests;

public class EnrichmentStoreRepositoryTests
{
    private readonly CertificatesStoreDbContext _dbContext;
    private readonly EnrichmentStoreRepository _sut;
    private readonly string _dbName;

    public EnrichmentStoreRepositoryTests()
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
        var data = new EnrichmentData
        {
            Data = Guid.NewGuid().ToString(),
            CreatedBy = Guid.NewGuid().ToString()
        };
        var token = new CancellationTokenSource().Token;
        await using (var context = CreateContext())
        {
            context.GeneralCertificate.Add(new()
            {
                Id = Guid.NewGuid(),
                CreatedBy = Guid.NewGuid().ToString(),
                Data = Guid.NewGuid().ToString(),
                GeneralCertificateId = gcId
            });
            await context.SaveChangesAsync(token);
        }

        // act
        var actual = await _sut.CreateAsync(gcId, data, token);

        // assert
        actual.Should().Be(data);
        await using (var context = CreateContext())
        {
            var enrichment = await context.EnrichmentData.Include(e => e.GeneralCertificate).SingleAsync();
            enrichment.Should().BeEquivalentTo(data, opt => opt.IgnoringCyclicReferences());
        }
    }

    private CertificatesStoreDbContext CreateContext()
    {
        var builder = new DbContextOptionsBuilder<CertificatesStoreDbContext>()
            .UseInMemoryDatabase(_dbName);
        return new(builder.Options);
    }
}
