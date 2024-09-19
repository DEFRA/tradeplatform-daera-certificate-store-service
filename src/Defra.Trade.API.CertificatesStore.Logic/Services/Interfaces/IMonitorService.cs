// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Logic.Models;

namespace Defra.Trade.API.CertificatesStore.Logic.Services.Interfaces;

/// <summary>
/// Monitor service
/// </summary>
public interface IMonitorService
{
    /// <summary>
    /// Get a health report containing database context information
    /// </summary>
    /// <returns>The health report</returns>
    public Task<HealthReportResponse> GetReport();
}