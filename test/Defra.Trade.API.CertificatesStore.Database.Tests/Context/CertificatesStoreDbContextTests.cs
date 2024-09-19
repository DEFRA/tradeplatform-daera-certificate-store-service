// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Database.Context;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Xunit;

namespace Defra.Trade.API.CertificatesStore.Database.Tests.Context;

public class CertificatesStoreDbContextTests
{
    private readonly CertificatesStoreDbContext _sut;

    public CertificatesStoreDbContextTests()
    {
        var builder = new DbContextOptionsBuilder<CertificatesStoreDbContext>()
            .UseSqlServer();
        _sut = new(builder.Options);
    }

    [Fact]
    public void ThereShouldBeNoPendingChangesForMigrations()
    {
        // arrange
        var migrationsAssembly = _sut.GetService<IMigrationsAssembly>();

        // act
        bool hasDifferences = false;
        if (migrationsAssembly.ModelSnapshot != null)
        {
            var snapshotModel = migrationsAssembly.ModelSnapshot?.Model;

            if (snapshotModel is IMutableModel mutableModel)
            {
                snapshotModel = mutableModel.FinalizeModel();
            }

            snapshotModel.Should().NotBeNull();

            snapshotModel = _sut.GetService<IModelRuntimeInitializer>().Initialize(snapshotModel!);
            hasDifferences = _sut.GetService<IMigrationsModelDiffer>().HasDifferences(
                snapshotModel.GetRelationalModel(),
                _sut.GetService<IDesignTimeModel>().Model.GetRelationalModel());
        }

        // assert
        hasDifferences.Should().Be(false);
    }

    [Fact]
    public void SaveChanges_WhenCalled_ShouldThrow()
    {
        // act
        var act = () => _sut.SaveChanges();

        // assert
        act.Should().Throw<InvalidOperationException>("method not allowed, use SaveChangesAsync.");
    }
}
