using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pigeon_report_service.Migrations
{
    /// <inheritdoc />
    public partial class Ids_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "report",
                table: "Report",
                newName: "ReportId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "report",
                table: "Info",
                newName: "InfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReportId",
                schema: "report",
                table: "Report",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "InfoId",
                schema: "report",
                table: "Info",
                newName: "Id");
        }
    }
}
