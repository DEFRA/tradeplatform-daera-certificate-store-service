// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Database.Context;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Xunit;

namespace Defra.Trade.API.CertificatesStore.Database.Tests.Migrations;

public class MigrationsTests
{
    private readonly CertificatesStoreDbContext _context;

    public MigrationsTests()
    {
        var builder = new DbContextOptionsBuilder<CertificatesStoreDbContext>()
            .UseSqlServer();

        _context = new CertificatesStoreDbContext(builder.Options);
    }

    public static TheoryData<string, string> GetMigrationNames()
    {
        var testClass = new MigrationsTests();
        var migrationsAssembly = testClass._context.GetService<IMigrationsAssembly>();

        var theoryData = new TheoryData<string, string>();

        var currentMigration = migrationsAssembly.Migrations
            .OrderByDescending(kvp => kvp.Key)
            .First()
            .Value;

        foreach (var migration in migrationsAssembly.Migrations.Values)
        {
            theoryData.Add("0", migration.Name);
            theoryData.Add(currentMigration.Name, migration == currentMigration ? "0" : migration.Name);
        }

        return theoryData;
    }

    [Theory]
    [MemberData(nameof(GetMigrationNames))]
    public void ShouldBeAbleToGenerateAScriptToMigrate(string fromMigration, string toMigration)
    {
        // arrange
        var migrator = _context.GetService<IMigrator>();

        // act
        string script = migrator.GenerateScript(fromMigration, toMigration);

        // assert
        script.Should().NotBeNullOrEmpty();
    }
}
