// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Diagnostics;
using System.Linq;
using Defra.Trade.API.CertificatesStore.Database.Services.Interfaces;
using Defra.Trade.API.CertificatesStore.Logic.Models;
using Defra.Trade.API.CertificatesStore.Logic.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Defra.Trade.API.CertificatesStore.Logic.Services;

/// <inheritdoc />
/// <summary>
/// Initializes and new instance of <see cref="MonitorService"/>
/// </summary>
/// <param name="dbHealthCheckService">The database health check service</param>
/// <exception cref="ArgumentNullException"></exception>
public class MonitorService(IDbHealthCheckService dbHealthCheckService) : IMonitorService
{
    private readonly IDbHealthCheckService _dbHealthCheckService = dbHealthCheckService
        ?? throw new ArgumentNullException(nameof(dbHealthCheckService));

    /// <inheritdoc />
    public async Task<HealthReportResponse> GetReport()
    {
        var sw = Stopwatch.StartNew();

        HealthReportResponse healthReportResponse = new();
        var dbEntry = new HealthCheckResultEntry("CertificatesStoreDbContext");

        try
        {
            var databaseInfo = await _dbHealthCheckService.GetContextInfo();

            var healthStatus = databaseInfo.PendingMigrations.Any() ? HealthStatus.Unhealthy : HealthStatus.Healthy;

            dbEntry.Data = databaseInfo;
            dbEntry.DurationMs = (int)sw.ElapsedMilliseconds;
            dbEntry.Status = healthStatus;

            healthReportResponse.Entries.Add(dbEntry);
            healthReportResponse.TotalDurationMs = (int)sw.ElapsedMilliseconds;
            healthReportResponse.Status = healthReportResponse.Entries
                .Exists(x => x.Status == HealthStatus.Unhealthy) ?
                HealthStatus.Unhealthy :
                HealthStatus.Healthy;
        }
        catch (SqlException ex)
        {
            dbEntry.ExceptionMessage = $"SqlException: {ex.Message}";
            dbEntry.Status = HealthStatus.Unhealthy;
            dbEntry.DurationMs = (int)sw.ElapsedMilliseconds;

            healthReportResponse.Status = HealthStatus.Unhealthy;
            healthReportResponse.TotalDurationMs = (int)sw.ElapsedMilliseconds;
            healthReportResponse.Entries.Add(dbEntry);
        }

        return healthReportResponse;
    }
}
