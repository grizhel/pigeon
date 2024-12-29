using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pigeon_crud_service.Migrations
{
    /// <inheritdoc />
    public partial class Ids_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactInformation_Contact_ContactId",
                schema: "crud",
                table: "ContactInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contact",
                schema: "crud",
                table: "Contact");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "crud",
                table: "Location",
                newName: "LocationId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "crud",
                table: "Firm",
                newName: "FirmId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "crud",
                table: "ContactInformation",
                newName: "ContactInformationId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "crud",
                table: "Contact",
                newName: "LocationId");

            migrationBuilder.AddColumn<Guid>(
                name: "ContactId",
                schema: "crud",
                table: "Contact",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contact",
                schema: "crud",
                table: "Contact",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_LocationId",
                schema: "crud",
                table: "Contact",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Location_LocationId",
                schema: "crud",
                table: "Contact",
                column: "LocationId",
                principalSchema: "crud",
                principalTable: "Location",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactInformation_Contact_ContactId",
                schema: "crud",
                table: "ContactInformation",
                column: "ContactId",
                principalSchema: "crud",
                principalTable: "Contact",
                principalColumn: "ContactId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Location_LocationId",
                schema: "crud",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactInformation_Contact_ContactId",
                schema: "crud",
                table: "ContactInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contact",
                schema: "crud",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_LocationId",
                schema: "crud",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "ContactId",
                schema: "crud",
                table: "Contact");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                schema: "crud",
                table: "Location",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "FirmId",
                schema: "crud",
                table: "Firm",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ContactInformationId",
                schema: "crud",
                table: "ContactInformation",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                schema: "crud",
                table: "Contact",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contact",
                schema: "crud",
                table: "Contact",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactInformation_Contact_ContactId",
                schema: "crud",
                table: "ContactInformation",
                column: "ContactId",
                principalSchema: "crud",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
