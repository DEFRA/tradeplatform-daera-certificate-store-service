// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Defra.Trade.API.CertificatesStore.Database.Migrations;

/// <inheritdoc />
public partial class AddGcDocumentLocation : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "GeneralCertificateDocument",
            schema: "gcs",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                GeneralCertificateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                PackingListFileLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                CreatedSystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastUpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastUpdatedSystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IsActive = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralCertificateDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneralCertificateDocument_GeneralCertificate_GeneralCertificateId",
                        column: x => x.GeneralCertificateId,
                        principalSchema: "gcs",
                        principalTable: "GeneralCertificate",
                        principalColumn: "Id");
                });

        migrationBuilder.CreateIndex(
            name: "IX_GeneralCertificateDocument_GeneralCertificateId",
            schema: "gcs",
            table: "GeneralCertificateDocument",
            column: "GeneralCertificateId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "GeneralCertificateDocument",
            schema: "gcs");
    }
}