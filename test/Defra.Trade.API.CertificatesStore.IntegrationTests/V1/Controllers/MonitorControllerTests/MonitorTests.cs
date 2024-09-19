// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Net.Http.Json;
using Defra.Trade.API.CertificatesStore.Database.Models;
using Defra.Trade.API.CertificatesStore.IntegrationTests.Infrastructure;
using Defra.Trade.API.CertificatesStore.IntegrationTests.V1.Models;
using Defra.Trade.API.CertificatesStore.Logic.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Defra.Trade.API.CertificatesStore.IntegrationTests.V1.Controllers.MonitorControllerTests;

public class MonitorTests(CertificatesStoreApplicationFactory<Startup> webApplicationFactory)
    : IClassFixture<CertificatesStoreApplicationFactory<Startup>>
{
    private readonly CertificatesStoreApplicationFactory<Startup> _webApplicationFactory = webApplicationFactory
        ?? throw new ArgumentNullException(nameof(webApplicationFactory));

    [Fact]
    public async Task Monitor_WhenHealthy_ShouldReturn200AndHealthy()
    {
        // Arrange
        var client = _webApplicationFactory.CreateClient();

        var monitorResponse = new HealthReportResponse()
        {
            Status = HealthStatus.Healthy,
            Entries =
            [
                new()
                {
                    Key = "first",
                    Status = HealthStatus.Healthy,
                    Description = "description",
                    DurationMs = 3,
                    ExceptionMessage = new ArithmeticException().Message,
                    Data = new DatabaseInfo()
                    {
                        CanConnect = true,
                        CurrentMigration = "currentMigration",
                        DatabaseName = "databaseName",
                        PendingMigrations = new List<string>()
                    }
                }
            ],
            TotalDurationMs = 4
        };

        _webApplicationFactory.MonitorService
            .Setup(x => x.GetReport())
            .ReturnsAsync(monitorResponse);

        // Act
        var response = await client.GetAsync("monitor");

        // Assert
        var content = await response.Content.ReadFromJsonAsync<TestHealthReportResponse>();
        content.Should().NotBeNull();
        content!.Status.Should().Be(HealthStatus.Healthy);
        content.Entries.Count.Should().Be(1);
        content.TotalDurationMs.Should().BeGreaterThanOrEqualTo(0);

        var responseEntry = content.Entries[0];
        var sourceEntry = content.Entries[0];

        responseEntry.Key.Should().Be(sourceEntry.Key);
        responseEntry.Status.Should().Be(sourceEntry.Status);
        responseEntry.Description.Should().Be(sourceEntry.Description);
        responseEntry.DurationMs.Should().Be(sourceEntry.DurationMs);
        responseEntry.ExceptionMessage.Should().NotBeNull();
        responseEntry.Data.CurrentMigration.Should().Be(sourceEntry.Data.CurrentMigration);
        responseEntry.Data.CanConnect.Should().Be(sourceEntry.Data.CanConnect);
        responseEntry.Data.DatabaseName.Should().Be(sourceEntry.Data.DatabaseName);
        responseEntry.Data.PendingMigrations.Count().Should().Be(0);
        responseEntry.Tags.Should().BeEmpty();
    }

    [Fact]
    public async Task Monitor_WhenUnhealthy_ShouldReturn500AndUnhealthy()
    {
        // Arrange
        var client = _webApplicationFactory.CreateClient();

        var monitorResponse = new HealthReportResponse()
        {
            Status = HealthStatus.Unhealthy,
            Entries =
            [
                new()
                {
                    Key = "first",
                    Status = HealthStatus.Healthy,
                    Description = "description",
                    DurationMs = 3,
                    ExceptionMessage = new ArithmeticException().Message,
                    Data = new DatabaseInfo()
                    {
                        CanConnect = true,
                        CurrentMigration = "currentMigration",
                        DatabaseName = "databaseName",
                        PendingMigrations = new List<string>()
                        {
                            "pendingMigration"
                        }
                    }
                }
            ],
            TotalDurationMs = 4
        };

        _webApplicationFactory.MonitorService
            .Setup(x => x.GetReport())
            .ReturnsAsync(monitorResponse);

        // Act
        var response = await client.GetAsync("monitor");

        // Assert
        var content = await response.Content.ReadFromJsonAsync<TestHealthReportResponse>();
        content.Should().NotBeNull();
        content!.Status.Should().Be(HealthStatus.Unhealthy);
        content.Entries.Count.Should().Be(1);
        content.TotalDurationMs.Should().BeGreaterThanOrEqualTo(0);

        var responseEntry = content.Entries[0];
        var sourceEntry = content.Entries[0];

        responseEntry.Key.Should().Be(sourceEntry.Key);
        responseEntry.Status.Should().Be(sourceEntry.Status);
        responseEntry.Description.Should().Be(sourceEntry.Description);
        responseEntry.DurationMs.Should().Be(sourceEntry.DurationMs);
        responseEntry.ExceptionMessage.Should().NotBeNull();
        responseEntry.Data.CurrentMigration.Should().Be(sourceEntry.Data.CurrentMigration);
        responseEntry.Data.CanConnect.Should().Be(sourceEntry.Data.CanConnect);
        responseEntry.Data.DatabaseName.Should().Be(sourceEntry.Data.DatabaseName);
        responseEntry.Data.PendingMigrations.Count().Should().Be(1);
        responseEntry.Tags.Should().BeEmpty();
    }
}
