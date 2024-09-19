// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Data.Common;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;

namespace Defra.Trade.API.CertificatesStore.Database.Tests.TestHelpers;

internal class DbContextTestHelpers
{
    protected DbContextTestHelpers()
    {
    }

    public static DbContext GetNewMockedDbContext(
        bool canConnect,
        List<string> appliedMigrations,
        List<string> pendingMigrations,
        DbConnection dbConnection)
    {
        var dbContext = new Mock<DbContext>(MockBehavior.Strict);
        var dependencies = new Mock<IRelationalDatabaseFacadeDependencies>(MockBehavior.Strict);
        var services = new Mock<IServiceProvider>(MockBehavior.Strict);
        var dbCreator = new Mock<IDatabaseCreator>(MockBehavior.Strict);
        var relationalConnection = new Mock<IRelationalConnection>(MockBehavior.Strict);
        var history = new Mock<IHistoryRepository>(MockBehavior.Strict);
        var migrations = new Mock<IMigrationsAssembly>(MockBehavior.Strict);

        dbContext.Setup(m => m.Database).CallBase();
        dbContext.As<IInfrastructure<IServiceProvider>>().Setup(m => m.Instance).Returns(services.Object);

        services.Setup(m => m.GetService(typeof(IDatabaseFacadeDependencies))).Returns(dependencies.Object);
        services.Setup(m => m.GetService(typeof(IHistoryRepository))).Returns(history.Object);
        services.Setup(m => m.GetService(typeof(IMigrationsAssembly))).Returns(migrations.Object);

        dependencies.Setup(m => m.DatabaseCreator).Returns(dbCreator.Object);
        dependencies.Setup(m => m.RelationalConnection).Returns(relationalConnection.Object);

        dbCreator.Setup(m => m.CanConnectAsync(default)).ReturnsAsync(canConnect);
        relationalConnection.Setup(m => m.DbConnection).Returns(dbConnection);

        history.Setup(m => m.GetAppliedMigrationsAsync(default)).ReturnsAsync(appliedMigrations
            .Select(x => new HistoryRow(x, string.Empty))
            .ToList());

        migrations.Setup(m => m.Migrations).Returns(pendingMigrations
            .ToDictionary(x => x, x => new Mock<TypeInfo>().Object));

        return dbContext.Object;
    }
}