// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Diagnostics.CodeAnalysis;
using Defra.Trade.API.CertificatesStore.Logic.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Defra.Trade.API.CertificatesStore.V2.Controllers;

/// <summary>
/// Health controller.
/// </summary>
/// <remarks>
/// Object creator for the type HealthController.
/// </remarks>
/// <exception cref="ArgumentNullException"></exception>
[ExcludeFromCodeCoverage(Justification = "Unable to mock HealthCheckService.")]
[ApiVersion("1")]
[ApiController]
[Route("api")]
public class HealthController(HealthCheckService healthCheckService) : ControllerBase
{
    private readonly HealthCheckService _healthCheckService = healthCheckService;

    [HttpGet("health", Name = "CheckHealth")]
    [ProducesResponseType(typeof(HealthReportResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(HealthReportResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Health()
    {
        var report = await _healthCheckService.CheckHealthAsync();
        return report.Status == HealthStatus.Healthy
                   ? Ok(report)
                   : StatusCode(500, report);
    }
}
