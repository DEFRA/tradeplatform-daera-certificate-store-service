// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Logic.Models;
using Defra.Trade.API.CertificatesStore.Logic.Services.Interfaces;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Defra.Trade.API.CertificatesStore.V2.Controllers;

/// <summary>
/// Monitor controller.
/// </summary>
/// <remarks>
/// Object creator for the type HealthController.
/// </remarks>
/// <exception cref="ArgumentNullException"></exception>
[ApiVersion("1")]
[ApiController]
public class MonitorController(IMonitorService monitorService) : ControllerBase
{
    private readonly IMonitorService _monitorService = monitorService ?? throw new ArgumentNullException(nameof(monitorService));

    /// <summary>
    /// Application monitoring service to look for downstream applications health status.
    /// </summary>
    /// <returns>Health report response</returns>
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("/monitor")]
    [ProducesResponseType(typeof(HealthReportResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(HealthReportResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Index()
    {
        var report = await _monitorService.GetReport();

        bool entriesHealthy = !report.Entries.Exists(x => x.Status == HealthStatus.Unhealthy);
        bool reportIsHealthy = report.Status == HealthStatus.Healthy && entriesHealthy;

        return reportIsHealthy
            ? Ok(report)
            : StatusCode(500, report);
    }
}
