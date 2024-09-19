// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Defra.Trade.API.CertificatesStore.Database.Migrations;

/// <inheritdoc />
public partial class AddNonClusteredIndexToGcId : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateIndex(
            name: "IX_GeneralCertificate_GeneralCertificateId",
            schema: "gcs",
            table: "GeneralCertificate",
            column: "GeneralCertificateId")
            .Annotation("SqlServer:Clustered", false);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_GeneralCertificate_GeneralCertificateId",
            schema: "gcs",
            table: "GeneralCertificate");
    }
}
