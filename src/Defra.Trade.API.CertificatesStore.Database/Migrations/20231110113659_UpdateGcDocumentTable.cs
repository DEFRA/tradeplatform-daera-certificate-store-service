// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Defra.Trade.API.CertificatesStore.Database.Migrations;

/// <inheritdoc />
public partial class UpdateGcDocumentTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "PackingListFileLocation",
            schema: "gcs",
            table: "GeneralCertificateDocument",
            newName: "Url");

        migrationBuilder.AddColumn<string>(
            name: "DocumentId",
            schema: "gcs",
            table: "GeneralCertificateDocument",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "FileName",
            schema: "gcs",
            table: "GeneralCertificateDocument",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<int>(
            name: "ListAgencyId",
            schema: "gcs",
            table: "GeneralCertificateDocument",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<int>(
            name: "TypeCode",
            schema: "gcs",
            table: "GeneralCertificateDocument",
            type: "int",
            nullable: false,
            defaultValue: 0);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "DocumentId",
            schema: "gcs",
            table: "GeneralCertificateDocument");

        migrationBuilder.DropColumn(
            name: "FileName",
            schema: "gcs",
            table: "GeneralCertificateDocument");

        migrationBuilder.DropColumn(
            name: "ListAgencyId",
            schema: "gcs",
            table: "GeneralCertificateDocument");

        migrationBuilder.DropColumn(
            name: "TypeCode",
            schema: "gcs",
            table: "GeneralCertificateDocument");

        migrationBuilder.RenameColumn(
            name: "Url",
            schema: "gcs",
            table: "GeneralCertificateDocument",
            newName: "PackingListFileLocation");
    }
}