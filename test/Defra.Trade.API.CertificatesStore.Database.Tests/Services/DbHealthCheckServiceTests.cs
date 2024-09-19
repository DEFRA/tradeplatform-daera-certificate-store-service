// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Data.Common;
using Defra.Trade.API.CertificatesStore.Database.Context;
using Defra.Trade.API.CertificatesStore.Database.Services;
using Defra.Trade.API.CertificatesStore.Database.Tests.TestHelpers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Defra.Trade.API.CertificatesStore.Database.Tests.Services;

public class DbHealthCheckServiceTests
{
    private DbHealthCheckService<DbContext>? _sut;

    [Fact]
    public void Ctor_NullArgs_Throws()
    {
        // Act && Assert
        Assert.Throws<ArgumentNullException>(
            () => new DbHealthCheckService<CertificatesStoreDbContext>(null!));
    }

    /// <remarks>Unhealthy == pendingMigrations !== 0</remarks>
    [Fact]
    public async Task GetContextInfo_UnhealthyDbWithoutAppliedMigrations_ReturnsValidDatabaseInfo()
    {
        // Arrange
        var appliedMigrations = new List<string>();
        var pendingMigrations = new List<string> { "pendingMigration0", "pendingMigration1" };
        var dbConnection = new Mock<DbConnection>();
        dbConnection.Setup(x => x.Database).Returns("db0");

        var contextMock = DbContextTestHelpers.GetNewMockedDbContext(
            true,
            appliedMigrations,
            pendingMigrations,
            dbConnection.Object);

        _sut = new DbHealthCheckService<DbContext>(contextMock);

        // Act
        var result = await _sut.GetContextInfo();

        // Assert
        result.CanConnect.Should().BeTrue();
        result.CurrentMigration.Should().BeNullOrEmpty();
        result.PendingMigrations.Count().Should().Be(2);
        result.PendingMigrations.ToList()[0].Should().Be("pendingMigration0");
        result.PendingMigrations.ToList()[1].Should().Be("pendingMigration1");
        result.DatabaseName.Should().Be("db0");
    }

    /// <remarks>Unhealthy == pendingMigrations !== 0</remarks>
    [Fact]
    public async Task GetContextInfo_UnhealthyDbWithAppliedMigrations_ReturnsValidDatabaseInfo()
    {
        // Arrange
        var appliedMigrations = new List<string> { "migration0", "migration1" };
        var pendingMigrations = new List<string> { "pendingMigration0", "pendingMigration1" };
        var dbConnection = new Mock<DbConnection>();
        dbConnection.Setup(x => x.Database).Returns("db0");

        var contextMock = DbContextTestHelpers.GetNewMockedDbContext(
            true,
            appliedMigrations,
            pendingMigrations,
            dbConnection.Object);

        _sut = new DbHealthCheckService<DbContext>(contextMock);

        // Act
        var result = await _sut.GetContextInfo();

        // Assert
        result.CanConnect.Should().BeTrue();
        result.CurrentMigration.Should().Be("migration1");
        result.PendingMigrations.Count().Should().Be(2);
        result.PendingMigrations.ToList()[0].Should().Be("pendingMigration0");
        result.PendingMigrations.ToList()[1].Should().Be("pendingMigration1");
        result.DatabaseName.Should().Be("db0");
    }

    [Fact]
    public async Task GetContextInfo_HealthyDbWithAppliedMigrations_ReturnsValidDatabaseInfo()
    {
        // Arrange
        var appliedMigrations = new List<string> { "migration0", "migration1" };
        var pendingMigrations = new List<string>();
        var dbConnection = new Mock<DbConnection>();
        dbConnection.Setup(x => x.Database).Returns("db0");

        var contextMock = DbContextTestHelpers.GetNewMockedDbContext(
            true,
            appliedMigrations,
            pendingMigrations,
            dbConnection.Object);

        _sut = new DbHealthCheckService<DbContext>(contextMock);

        // Act
        var result = await _sut.GetContextInfo();

        // Assert
        result.CanConnect.Should().BeTrue();
        result.CurrentMigration.Should().Be("migration1");
        result.PendingMigrations.Should().BeNullOrEmpty();
        result.DatabaseName.Should().Be("db0");
    }
}
