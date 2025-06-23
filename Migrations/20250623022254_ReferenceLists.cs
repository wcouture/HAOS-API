using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAOS_API.Migrations
{
    /// <inheritdoc />
    public partial class ReferenceLists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Days",
                table: "SegmentData");

            migrationBuilder.DropColumn(
                name: "Circuits",
                table: "ProgramDayData");

            migrationBuilder.DropColumn(
                name: "Segments",
                table: "ProgramData");

            migrationBuilder.DropColumn(
                name: "Workouts",
                table: "CircuitData");

            migrationBuilder.AddColumn<int>(
                name: "CircuitId",
                table: "WorkoutData",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrainingProgramId",
                table: "SegmentData",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProgramSegmentId",
                table: "ProgramDayData",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProgramDayId",
                table: "CircuitData",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutData_CircuitId",
                table: "WorkoutData",
                column: "CircuitId");

            migrationBuilder.CreateIndex(
                name: "IX_SegmentData_TrainingProgramId",
                table: "SegmentData",
                column: "TrainingProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDayData_ProgramSegmentId",
                table: "ProgramDayData",
                column: "ProgramSegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CircuitData_ProgramDayId",
                table: "CircuitData",
                column: "ProgramDayId");

            migrationBuilder.AddForeignKey(
                name: "FK_CircuitData_ProgramDayData_ProgramDayId",
                table: "CircuitData",
                column: "ProgramDayId",
                principalTable: "ProgramDayData",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramDayData_SegmentData_ProgramSegmentId",
                table: "ProgramDayData",
                column: "ProgramSegmentId",
                principalTable: "SegmentData",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SegmentData_ProgramData_TrainingProgramId",
                table: "SegmentData",
                column: "TrainingProgramId",
                principalTable: "ProgramData",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutData_CircuitData_CircuitId",
                table: "WorkoutData",
                column: "CircuitId",
                principalTable: "CircuitData",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CircuitData_ProgramDayData_ProgramDayId",
                table: "CircuitData");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramDayData_SegmentData_ProgramSegmentId",
                table: "ProgramDayData");

            migrationBuilder.DropForeignKey(
                name: "FK_SegmentData_ProgramData_TrainingProgramId",
                table: "SegmentData");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutData_CircuitData_CircuitId",
                table: "WorkoutData");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutData_CircuitId",
                table: "WorkoutData");

            migrationBuilder.DropIndex(
                name: "IX_SegmentData_TrainingProgramId",
                table: "SegmentData");

            migrationBuilder.DropIndex(
                name: "IX_ProgramDayData_ProgramSegmentId",
                table: "ProgramDayData");

            migrationBuilder.DropIndex(
                name: "IX_CircuitData_ProgramDayId",
                table: "CircuitData");

            migrationBuilder.DropColumn(
                name: "CircuitId",
                table: "WorkoutData");

            migrationBuilder.DropColumn(
                name: "TrainingProgramId",
                table: "SegmentData");

            migrationBuilder.DropColumn(
                name: "ProgramSegmentId",
                table: "ProgramDayData");

            migrationBuilder.DropColumn(
                name: "ProgramDayId",
                table: "CircuitData");

            migrationBuilder.AddColumn<string>(
                name: "Days",
                table: "SegmentData",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Circuits",
                table: "ProgramDayData",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Segments",
                table: "ProgramData",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Workouts",
                table: "CircuitData",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
