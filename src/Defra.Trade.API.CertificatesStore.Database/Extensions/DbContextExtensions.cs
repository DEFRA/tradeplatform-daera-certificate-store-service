// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Diagnostics;
using Defra.Trade.API.CertificatesStore.Database.Models;

namespace Defra.Trade.API.CertificatesStore.Database.Extensions;

public static class DbContextExtensions
{
    public static async Task<DatabaseInfo> GetInfo(this DbContext context)
    {
        bool canConnect = await context.Database.CanConnectAsync();
        string databaseName = context.Database.GetDbConnection().Database;
        var appliedMigrations = await context.Database.GetAppliedMigrationsAsync();
        Debug.Assert(appliedMigrations != null);
        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();

        var databaseInfo = new DatabaseInfo()
        {
            CanConnect = canConnect,
            DatabaseName = databaseName,
            CurrentMigration = appliedMigrations.Any() ? appliedMigrations.Last() : null!,
            PendingMigrations = pendingMigrations
        };

        return databaseInfo;
    }
}