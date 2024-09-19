// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Defra.Trade.API.CertificatesStore.Database.Migrations;

/// <inheritdoc />
public partial class AddRetrievalColumn : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(
            name: "Retrieved",
            schema: "gcs",
            table: "GeneralCertificateDocument",
            type: "datetime2",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Retrieved",
            schema: "gcs",
            table: "GeneralCertificateDocument");
    }
}
