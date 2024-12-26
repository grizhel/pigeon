using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pigeon_crud_service.Migrations
{
    /// <inheritdoc />
    public partial class contact_firm_nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Firm_FirmId",
                schema: "general",
                table: "Contact");

            migrationBuilder.AlterColumn<Guid>(
                name: "FirmId",
                schema: "general",
                table: "Contact",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Firm_FirmId",
                schema: "general",
                table: "Contact",
                column: "FirmId",
                principalSchema: "general",
                principalTable: "Firm",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Firm_FirmId",
                schema: "general",
                table: "Contact");

            migrationBuilder.AlterColumn<Guid>(
                name: "FirmId",
                schema: "general",
                table: "Contact",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Firm_FirmId",
                schema: "general",
                table: "Contact",
                column: "FirmId",
                principalSchema: "general",
                principalTable: "Firm",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
