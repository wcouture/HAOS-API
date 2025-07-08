using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAOS_API.Migrations
{
    /// <inheritdoc />
    public partial class ExerciseTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ExerciseData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedDate",
                table: "CompletedWorkoutData",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "CompletedWorkoutData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeightUsed",
                table: "CompletedWorkoutData",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "ExerciseData");

            migrationBuilder.DropColumn(
                name: "CompletedDate",
                table: "CompletedWorkoutData");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "CompletedWorkoutData");

            migrationBuilder.DropColumn(
                name: "WeightUsed",
                table: "CompletedWorkoutData");
        }
    }
}
