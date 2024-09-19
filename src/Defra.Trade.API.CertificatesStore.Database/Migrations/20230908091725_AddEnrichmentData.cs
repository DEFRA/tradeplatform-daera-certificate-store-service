// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Defra.Trade.API.CertificatesStore.Database.Migrations;

/// <inheritdoc />
public partial class AddEnrichmentData : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTimeOffset>(
            name: "LastUpdatedOn",
            schema: "gcs",
            table: "GeneralCertificate",
            type: "datetimeoffset",
            nullable: true,
            oldClrType: typeof(DateTimeOffset),
            oldType: "datetimeoffset");

        migrationBuilder.CreateTable(
            name: "EnrichmentData",
            schema: "gcs",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                GeneralCertificateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                SchemaVersion = table.Column<int>(type: "int", nullable: false),
                Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                CreatedSystem = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                LastUpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                LastUpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                LastUpdatedSystem = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                IsActive = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EnrichmentData", x => x.Id);
                table.ForeignKey(
                    name: "FK_EnrichmentData_GeneralCertificate_GeneralCertificateId",
                    column: x => x.GeneralCertificateId,
                    principalSchema: "gcs",
                    principalTable: "GeneralCertificate",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_EnrichmentData_GeneralCertificateId",
            schema: "gcs",
            table: "EnrichmentData",
            column: "GeneralCertificateId",
            unique: true,
            filter: "[GeneralCertificateId] IS NOT NULL");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "EnrichmentData",
            schema: "gcs");

        migrationBuilder.AlterColumn<DateTimeOffset>(
            name: "LastUpdatedOn",
            schema: "gcs",
            table: "GeneralCertificate",
            type: "datetimeoffset",
            nullable: false,
            defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
            oldClrType: typeof(DateTimeOffset),
            oldType: "datetimeoffset",
            oldNullable: true);
    }
}
