using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAOS_API.Migrations
{
    /// <inheritdoc />
    public partial class CircuitAndWorkoutDescriptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecommendedReps",
                table: "WorkoutData");

            migrationBuilder.DropColumn(
                name: "RecommendedSets",
                table: "WorkoutData");

            migrationBuilder.DropColumn(
                name: "RecommendedWeight",
                table: "WorkoutData");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "WorkoutData",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CircuitData",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Rounds",
                table: "CircuitData",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "WorkoutData");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CircuitData");

            migrationBuilder.DropColumn(
                name: "Rounds",
                table: "CircuitData");

            migrationBuilder.AddColumn<int>(
                name: "RecommendedReps",
                table: "WorkoutData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecommendedSets",
                table: "WorkoutData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecommendedWeight",
                table: "WorkoutData",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
