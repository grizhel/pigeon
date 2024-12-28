using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pigeon_report_service.Migrations
{
    /// <inheritdoc />
    public partial class updating_models_accordingTo_requirements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Dictionary<string, string>>(
                name: "Details",
                schema: "report",
                table: "Report",
                type: "hstore",
                nullable: false,
                oldClrType: typeof(Dictionary<string, string>),
                oldType: "hstore",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompleteDate",
                schema: "report",
                table: "Report",
                type: "timestamp",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                schema: "report",
                table: "Report",
                type: "timestamp",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReportStatus",
                schema: "report",
                table: "Report",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestDate",
                schema: "report",
                table: "Report",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompleteDate",
                schema: "report",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "DueDate",
                schema: "report",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "ReportStatus",
                schema: "report",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "RequestDate",
                schema: "report",
                table: "Report");

            migrationBuilder.AlterColumn<Dictionary<string, string>>(
                name: "Details",
                schema: "report",
                table: "Report",
                type: "hstore",
                nullable: true,
                oldClrType: typeof(Dictionary<string, string>),
                oldType: "hstore");
        }
    }
}
