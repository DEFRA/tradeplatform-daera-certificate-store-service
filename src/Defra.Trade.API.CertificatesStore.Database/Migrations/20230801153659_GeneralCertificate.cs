// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Defra.Trade.API.CertificatesStore.Database.Migrations;

/// <inheritdoc />
public partial class GeneralCertificate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "gcs");

        migrationBuilder.CreateTable(
            name: "GeneralCertificate",
            schema: "gcs",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                GeneralCertificateId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                SchemaVersion = table.Column<int>(type: "int", nullable: false),
                Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                CreatedSystem = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                LastUpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastUpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                LastUpdatedSystem = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                IsActive = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_GeneralCertificate", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "GeneralCertificate",
            schema: "gcs");
    }
}
