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
                name: "general");

            migrationBuilder.CreateTable(
                name: "Location",
                schema: "general",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(63)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Firm",
                schema: "general",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    LocationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Firm_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "general",
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                schema: "general",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(63)", nullable: false),
                    Surname = table.Column<string>(type: "varchar(63)", nullable: false),
                    FirmId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_Firm_FirmId",
                        column: x => x.FirmId,
                        principalSchema: "general",
                        principalTable: "Firm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactInfo",
                schema: "general",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContactId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContactType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactInfo_Contact_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "general",
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contact_FirmId",
                schema: "general",
                table: "Contact",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfo_ContactId",
                schema: "general",
                table: "ContactInfo",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Firm_LocationId",
                schema: "general",
                table: "Firm",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactInfo",
                schema: "general");

            migrationBuilder.DropTable(
                name: "Contact",
                schema: "general");

            migrationBuilder.DropTable(
                name: "Firm",
                schema: "general");

            migrationBuilder.DropTable(
                name: "Location",
                schema: "general");
        }
    }
}
