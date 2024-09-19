// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Database.Models;
using Defra.Trade.API.CertificatesStore.Database.Services.Interfaces;
using Defra.Trade.API.CertificatesStore.Logic.Services;
using Defra.Trade.API.CertificatesStore.Logic.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Defra.Trade.API.CertificatesStore.Logic.Tests.Services;

public class MonitorServiceTests
{
    private readonly Mock<IDbHealthCheckService> _dbHealthCheckServiceMock;
    private readonly MonitorService _sut;

    public MonitorServiceTests()
    {
        _dbHealthCheckServiceMock = new();
        _sut = new MonitorService(_dbHealthCheckServiceMock.Object);
    }

    [Fact]
    public void Ctor_NullArg_Throws()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(
            () => new MonitorService(null));
    }

    [Fact]
    public async Task GetReport_ZeroPendingMigrations_ReturnsHealthy()
    {
        // Arrange
        var sourceDbInfo = new DatabaseInfo()
        {
            CanConnect = true,
            CurrentMigration = "currentMigration",
            DatabaseName = "databaseName",
            PendingMigrations = new List<string>()
        };

        _dbHealthCheckServiceMock.Setup(x => x
            .GetContextInfo())
            .ReturnsAsync(sourceDbInfo);

        // Act
        var result = await _sut.GetReport();

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(HealthStatus.Healthy);
        result.TotalDurationMs.Should().BeGreaterThanOrEqualTo(0);
        result.Entries.Should().HaveCount(1);

        var dbResultEntry = result.Entries[0];
        dbResultEntry.Description.Should().BeNull();
        dbResultEntry.DurationMs.Should().BeGreaterThanOrEqualTo(0);
        dbResultEntry.ExceptionMessage.Should().BeNull();
        dbResultEntry.Key.Should().Be("CertificatesStoreDbContext");
        dbResultEntry.Status.Should().Be(HealthStatus.Healthy);
        dbResultEntry.Tags.Should().BeEmpty();
        dbResultEntry.Data.CanConnect.Should().BeTrue();
        dbResultEntry.Data.CurrentMigration.Should().Be(sourceDbInfo.CurrentMigration);
        dbResultEntry.Data.DatabaseName.Should().Be(sourceDbInfo.DatabaseName);
        dbResultEntry.Data.PendingMigrations.Should().BeEmpty();
    }

    [Fact]
    public async Task GetReport_WithPendingMigrations_ReturnsUnhealthy()
    {
        // Arrange
        var sourceDbInfo = new DatabaseInfo()
        {
            CanConnect = true,
            CurrentMigration = "currentMigration",
            DatabaseName = "databaseName",
            PendingMigrations = new List<string>()
            {
                "pendingMigration0",
                "pendingMigration1"
            }
        };

        _dbHealthCheckServiceMock.Setup(x => x
            .GetContextInfo())
            .ReturnsAsync(sourceDbInfo);

        // Act
        var result = await _sut.GetReport();

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(HealthStatus.Unhealthy);
        result.TotalDurationMs.Should().BeGreaterThanOrEqualTo(0);
        result.Entries.Should().HaveCount(1);

        var resultEntry = result.Entries[0];
        resultEntry.Description.Should().BeNull();
        resultEntry.DurationMs.Should().BeGreaterThanOrEqualTo(0);
        resultEntry.ExceptionMessage.Should().BeNull();
        resultEntry.Key.Should().Be("CertificatesStoreDbContext");
        resultEntry.Status.Should().Be(HealthStatus.Unhealthy);
        resultEntry.Tags.Should().BeEmpty();

        resultEntry.Data.CanConnect.Should().BeTrue();
        resultEntry.Data.CurrentMigration.Should().Be(sourceDbInfo.CurrentMigration);
        resultEntry.Data.DatabaseName.Should().Be(sourceDbInfo.DatabaseName);
        resultEntry.Data.PendingMigrations.Should().HaveCount(2);
        resultEntry.Data.PendingMigrations.ToList()[0].Should().Be(sourceDbInfo.PendingMigrations.ToList()[0]);
        resultEntry.Data.PendingMigrations.ToList()[1].Should().Be(sourceDbInfo.PendingMigrations.ToList()[1]);
    }

    [Fact]
    public async Task GetReport_WithSqlException_ReturnsUnhealthyWithExceptionMessage()
    {
        // Arrange
        _dbHealthCheckServiceMock.Setup(x => x
            .GetContextInfo())
            .ThrowsAsync(CreateSqlException());

        // Act
        var result = await _sut.GetReport();

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(HealthStatus.Unhealthy);
        result.TotalDurationMs.Should().BeGreaterThanOrEqualTo(0);
        result.Entries.Should().HaveCount(1);

        var resultEntry = result.Entries[0];
        resultEntry.Description.Should().BeNull();
        resultEntry.DurationMs.Should().BeGreaterThanOrEqualTo(0);
        resultEntry.ExceptionMessage.Should().StartWith("SqlException:");
        resultEntry.Key.Should().Be("CertificatesStoreDbContext");
        resultEntry.Status.Should().Be(HealthStatus.Unhealthy);
        resultEntry.Tags.Should().BeEmpty();
        resultEntry.Data.Should().BeNull();
    }

    private static SqlException CreateSqlException()
    {
        SqlException exception = null!;
        try
        {
            var conn = new SqlConnection(@"Data Source=.;Database=GUARANTEED_TO_FAIL;Connection Timeout=1");
            conn.Open();
        }
        catch (SqlException ex)
        {
            exception = ex;
        }

        return exception;
    }
}
