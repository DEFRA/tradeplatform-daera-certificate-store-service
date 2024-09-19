// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Database.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Defra.Trade.API.CertificatesStore.Database.Configuration;

public sealed class EnrichmentDataConfiguration : BaseTableConfiguration<EnrichmentData>
{
    public override void Configure(EntityTypeBuilder<EnrichmentData> builder)
    {
        builder.HasKey(e => e.Id);

        ConfigureBase(builder);

        builder.Property(e => e.Id)
            .HasDefaultValueSql("newsequentialid()");
        builder.Property(e => e.SchemaVersion)
            .IsRequired();
        builder.Property(e => e.Data)
            .IsRequired();

        builder.HasOne(e => e.GeneralCertificate)
            .WithOne(e => e.EnrichmentData)
            .HasForeignKey<EnrichmentData>("GeneralCertificateId")
            .IsRequired();
    }
}

