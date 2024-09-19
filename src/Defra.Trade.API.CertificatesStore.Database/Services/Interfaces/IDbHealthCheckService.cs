// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Database.Models;

namespace Defra.Trade.API.CertificatesStore.Database.Services.Interfaces;

/// <summary>
/// Database health check service
/// </summary>
public interface IDbHealthCheckService
{
    /// <summary>
    /// Gets information on the database context for health monitoring
    /// </summary>
    /// <returns>The database context information</returns>
    Task<DatabaseInfo> GetContextInfo();
}