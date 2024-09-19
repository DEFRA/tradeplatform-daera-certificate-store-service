// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Database.Models;

namespace Defra.Trade.API.CertificatesStore.Database.Context;

public class CertificatesStoreDbContext : DbContext
{
    public virtual DbSet<GeneralCertificate> GeneralCertificate { get; set; }
    public virtual DbSet<EnrichmentData> EnrichmentData { get; set; }
    public virtual DbSet<GeneralCertificateDocument> GeneralCertificateDocument { get; set; }

    public CertificatesStoreDbContext()
    {
    }

    public CertificatesStoreDbContext(DbContextOptions<CertificatesStoreDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("gcs");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CertificatesStoreDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }

    public override int SaveChanges()
    {
        throw new InvalidOperationException($"method not allowed, use {nameof(SaveChangesAsync)}");
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(true, cancellationToken);
    }
}