using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pigeon_crud_service.Migrations
{
    /// <inheritdoc />
    public partial class updating_models_accordingTo_requirements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Firm_Location_LocationId",
                schema: "general",
                table: "Firm");

            migrationBuilder.DropTable(
                name: "ContactInfo",
                schema: "general");

            migrationBuilder.AlterColumn<string>(
                name: "NVIAddress",
                schema: "general",
                table: "Location",
                type: "varchar(16)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                schema: "general",
                table: "Location",
                type: "varchar(160)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                schema: "general",
                table: "Firm",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateTable(
                name: "ContactInformation",
                schema: "general",
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
                        principalSchema: "general",
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactInformation_ContactId",
                schema: "general",
                table: "ContactInformation",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Firm_Location_LocationId",
                schema: "general",
                table: "Firm",
                column: "LocationId",
                principalSchema: "general",
                principalTable: "Location",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Firm_Location_LocationId",
                schema: "general",
                table: "Firm");

            migrationBuilder.DropTable(
                name: "ContactInformation",
                schema: "general");

            migrationBuilder.AlterColumn<string>(
                name: "NVIAddress",
                schema: "general",
                table: "Location",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(16)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                schema: "general",
                table: "Location",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(160)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                schema: "general",
                table: "Firm",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ContactInfo",
                schema: "general",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContactId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContactType = table.Column<int>(type: "integer", nullable: false),
                    Info = table.Column<string>(type: "text", nullable: false)
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
                name: "IX_ContactInfo_ContactId",
                schema: "general",
                table: "ContactInfo",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Firm_Location_LocationId",
                schema: "general",
                table: "Firm",
                column: "LocationId",
                principalSchema: "general",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
