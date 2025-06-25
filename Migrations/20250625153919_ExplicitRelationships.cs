using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAOS_API.Migrations
{
    /// <inheritdoc />
    public partial class ExplicitRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CircuitData_ProgramDayData_ProgramDayId",
                table: "CircuitData");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutData_CircuitData_CircuitId",
                table: "WorkoutData");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutData_ExerciseData_ExerciseRefId",
                table: "WorkoutData");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutData_ExerciseRefId",
                table: "WorkoutData");

            migrationBuilder.DropColumn(
                name: "ExerciseRefId",
                table: "WorkoutData");

            migrationBuilder.AlterColumn<int>(
                name: "CircuitId",
                table: "WorkoutData",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExerciseId",
                table: "WorkoutData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProgramId",
                table: "SegmentData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SegmentId",
                table: "ProgramDayData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ProgramDayId",
                table: "CircuitData",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CircuitData_ProgramDayData_ProgramDayId",
                table: "CircuitData",
                column: "ProgramDayId",
                principalTable: "ProgramDayData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutData_CircuitData_CircuitId",
                table: "WorkoutData",
                column: "CircuitId",
                principalTable: "CircuitData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CircuitData_ProgramDayData_ProgramDayId",
                table: "CircuitData");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutData_CircuitData_CircuitId",
                table: "WorkoutData");

            migrationBuilder.DropColumn(
                name: "ExerciseId",
                table: "WorkoutData");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "SegmentData");

            migrationBuilder.DropColumn(
                name: "SegmentId",
                table: "ProgramDayData");

            migrationBuilder.AlterColumn<int>(
                name: "CircuitId",
                table: "WorkoutData",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ExerciseRefId",
                table: "WorkoutData",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProgramDayId",
                table: "CircuitData",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutData_ExerciseRefId",
                table: "WorkoutData",
                column: "ExerciseRefId");

            migrationBuilder.AddForeignKey(
                name: "FK_CircuitData_ProgramDayData_ProgramDayId",
                table: "CircuitData",
                column: "ProgramDayId",
                principalTable: "ProgramDayData",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutData_CircuitData_CircuitId",
                table: "WorkoutData",
                column: "CircuitId",
                principalTable: "CircuitData",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutData_ExerciseData_ExerciseRefId",
                table: "WorkoutData",
                column: "ExerciseRefId",
                principalTable: "ExerciseData",
                principalColumn: "Id");
        }
    }
}
