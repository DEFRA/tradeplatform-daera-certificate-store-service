// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Database.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Defra.Trade.API.CertificatesStore.Database.Configuration;

public abstract class BaseTableConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseTable
{
    public abstract void Configure(EntityTypeBuilder<T> builder);

    protected static void ConfigureBase(EntityTypeBuilder<T> builder)
    {
        builder.Property(e => e.CreatedOn)
            .IsRequired();
        builder.Property(e => e.CreatedBy)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(e => e.CreatedSystem)
            .HasMaxLength(10);
        builder.Property(e => e.LastUpdatedOn)
            .IsRequired(false);
        builder.Property(e => e.LastUpdatedBy)
            .IsRequired(false)
            .HasMaxLength(100);
        builder.Property(e => e.LastUpdatedSystem)
            .IsRequired(false)
            .HasMaxLength(10);
        builder.Property(e => e.IsActive)
            .IsRequired(true);
    }
}
