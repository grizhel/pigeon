using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pigeon_crud_service.Migrations
{
    /// <inheritdoc />
    public partial class models_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adress",
                schema: "general",
                table: "Location",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NVIAdress",
                schema: "general",
                table: "Location",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Info",
                schema: "general",
                table: "ContactInfo",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                schema: "general",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "NVIAdress",
                schema: "general",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "Info",
                schema: "general",
                table: "ContactInfo");
        }
    }
}
