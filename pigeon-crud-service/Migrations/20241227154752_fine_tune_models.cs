using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pigeon_crud_service.Migrations
{
    /// <inheritdoc />
    public partial class fine_tune_models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "general",
                table: "Location",
                type: "varchar(64)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(63)");

            migrationBuilder.AddColumn<int>(
                name: "LocationType",
                schema: "general",
                table: "Location",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "general",
                table: "Firm",
                type: "varchar(128)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                schema: "general",
                table: "Contact",
                type: "varchar(64)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(63)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "general",
                table: "Contact",
                type: "varchar(64)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(63)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationType",
                schema: "general",
                table: "Location");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "general",
                table: "Location",
                type: "varchar(63)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(64)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "general",
                table: "Firm",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(128)");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                schema: "general",
                table: "Contact",
                type: "varchar(63)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(64)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "general",
                table: "Contact",
                type: "varchar(63)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(64)");
        }
    }
}
