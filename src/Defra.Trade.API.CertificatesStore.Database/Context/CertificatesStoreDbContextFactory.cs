// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Design;

namespace Defra.Trade.API.CertificatesStore.Database.Context;

[ExcludeFromCodeCoverage]
internal class CertificatesStoreDbContextFactory : IDesignTimeDbContextFactory<CertificatesStoreDbContext>
{
    public CertificatesStoreDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CertificatesStoreDbContext>();
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=trade-daera-certificates;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True;");

        return new CertificatesStoreDbContext(optionsBuilder.Options);
    }
}