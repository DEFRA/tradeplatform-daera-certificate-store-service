// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Database.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Defra.Trade.API.CertificatesStore.IntegrationTests.V1.Models;

internal sealed class TestHealthReportResponse
{
    [JsonPropertyName("status")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public HealthStatus Status { get; set; }

    [JsonPropertyName("entries")]
    public List<Entry> Entries { get; set; } = [];

    [JsonPropertyName("totalDurationMs")]
    public int TotalDurationMs { get; set; }
}

public sealed class Entry
{
    public string Key { get; set; } = null!;
    public DatabaseInfo Data { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ExceptionMessage { get; set; } = null!;
    public int DurationMs { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public HealthStatus Status { get; set; }

    public List<string> Tags { get; set; } = [];
}