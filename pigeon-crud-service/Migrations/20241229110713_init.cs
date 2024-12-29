using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pigeon_crud_service.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "crud");

            migrationBuilder.CreateTable(
                name: "Location",
                schema: "crud",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LocationType = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "varchar(64)", nullable: false),
                    NVIAddress = table.Column<string>(type: "varchar(16)", nullable: true),
                    Address = table.Column<string>(type: "varchar(160)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Firm",
                schema: "crud",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(128)", nullable: false),
                    LocationId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Firm_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "crud",
                        principalTable: "Location",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                schema: "crud",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(64)", nullable: false),
                    Surname = table.Column<string>(type: "varchar(64)", nullable: false),
                    FirmId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_Firm_FirmId",
                        column: x => x.FirmId,
                        principalSchema: "crud",
                        principalTable: "Firm",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContactInformation",
                schema: "crud",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContactId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContactType = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactInformation_Contact_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "crud",
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contact_FirmId",
                schema: "crud",
                table: "Contact",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactInformation_ContactId",
                schema: "crud",
                table: "ContactInformation",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Firm_LocationId",
                schema: "crud",
                table: "Firm",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactInformation",
                schema: "crud");

            migrationBuilder.DropTable(
                name: "Contact",
                schema: "crud");

            migrationBuilder.DropTable(
                name: "Firm",
                schema: "crud");

            migrationBuilder.DropTable(
                name: "Location",
                schema: "crud");
        }
    }
}
