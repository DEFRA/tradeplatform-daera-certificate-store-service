// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.API.CertificatesStore.Database.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Defra.Trade.API.CertificatesStore.Database.Configuration;

public sealed class GeneralCertificateConfiguration : BaseTableConfiguration<GeneralCertificate>
{
    public override void Configure(EntityTypeBuilder<GeneralCertificate> builder)
    {
        builder.HasKey(e => e.Id);

        ConfigureBase(builder);

        builder.Property(e => e.Id)
            .HasDefaultValueSql("newsequentialid()");
        builder.Property(e => e.GeneralCertificateId)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(e => e.SchemaVersion)
            .IsRequired();
        builder.Property(e => e.Data)
            .IsRequired();

        builder.HasOne(e => e.EnrichmentData)
            .WithOne(e => e.GeneralCertificate)
            .HasForeignKey<EnrichmentData>("GeneralCertificateId")
            .IsRequired(false);

        builder.HasIndex(e => e.GeneralCertificateId)
             .IsClustered(false);

        builder.HasIndex(e => e.LastUpdatedOn)
             .IsClustered(false);
    }
}

