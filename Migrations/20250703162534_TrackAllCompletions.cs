using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAOS_API.Migrations
{
    /// <inheritdoc />
    public partial class TrackAllCompletions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompletedCircuits",
                table: "AccountData",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CompletedDays",
                table: "AccountData",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CompletedPrograms",
                table: "AccountData",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CompletedSegments",
                table: "AccountData",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedCircuits",
                table: "AccountData");

            migrationBuilder.DropColumn(
                name: "CompletedDays",
                table: "AccountData");

            migrationBuilder.DropColumn(
                name: "CompletedPrograms",
                table: "AccountData");

            migrationBuilder.DropColumn(
                name: "CompletedSegments",
                table: "AccountData");
        }
    }
}
