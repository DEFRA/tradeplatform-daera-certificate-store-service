// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Collections.Generic;

namespace Defra.Trade.API.CertificatesStore.Database.Models;

public sealed class DatabaseInfo
{
    public bool CanConnect { get; set; }
    public string DatabaseName { get; set; } = string.Empty;
    public string CurrentMigration { get; set; } = string.Empty;
    public IEnumerable<string> PendingMigrations { get; set; } = new List<string>();
}