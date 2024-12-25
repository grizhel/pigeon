using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pigeon_crud_service.Migrations
{
    /// <inheritdoc />
    public partial class location_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NVIAdress",
                schema: "general",
                table: "Location",
                newName: "NVIAddress");

            migrationBuilder.RenameColumn(
                name: "Adress",
                schema: "general",
                table: "Location",
                newName: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NVIAddress",
                schema: "general",
                table: "Location",
                newName: "NVIAdress");

            migrationBuilder.RenameColumn(
                name: "Address",
                schema: "general",
                table: "Location",
                newName: "Adress");
        }
    }
}
