using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAOS_API.Migrations
{
    /// <inheritdoc />
    public partial class SpecifyCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramDayData_SegmentData_ProgramSegmentId",
                table: "ProgramDayData");

            migrationBuilder.DropForeignKey(
                name: "FK_SegmentData_ProgramData_TrainingProgramId",
                table: "SegmentData");

            migrationBuilder.DropIndex(
                name: "IX_SegmentData_TrainingProgramId",
                table: "SegmentData");

            migrationBuilder.DropIndex(
                name: "IX_ProgramDayData_ProgramSegmentId",
                table: "ProgramDayData");

            migrationBuilder.DropColumn(
                name: "TrainingProgramId",
                table: "SegmentData");

            migrationBuilder.DropColumn(
                name: "ProgramSegmentId",
                table: "ProgramDayData");

            migrationBuilder.RenameColumn(
                name: "ExerciseId",
                table: "WorkoutData",
                newName: "Exercise_Id");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutData_Exercise_Id",
                table: "WorkoutData",
                column: "Exercise_Id");

            migrationBuilder.CreateIndex(
                name: "IX_SegmentData_ProgramId",
                table: "SegmentData",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDayData_SegmentId",
                table: "ProgramDayData",
                column: "SegmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramDayData_SegmentData_SegmentId",
                table: "ProgramDayData",
                column: "SegmentId",
                principalTable: "SegmentData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SegmentData_ProgramData_ProgramId",
                table: "SegmentData",
                column: "ProgramId",
                principalTable: "ProgramData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutData_ExerciseData_Exercise_Id",
                table: "WorkoutData",
                column: "Exercise_Id",
                principalTable: "ExerciseData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramDayData_SegmentData_SegmentId",
                table: "ProgramDayData");

            migrationBuilder.DropForeignKey(
                name: "FK_SegmentData_ProgramData_ProgramId",
                table: "SegmentData");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutData_ExerciseData_Exercise_Id",
                table: "WorkoutData");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutData_Exercise_Id",
                table: "WorkoutData");

            migrationBuilder.DropIndex(
                name: "IX_SegmentData_ProgramId",
                table: "SegmentData");

            migrationBuilder.DropIndex(
                name: "IX_ProgramDayData_SegmentId",
                table: "ProgramDayData");

            migrationBuilder.RenameColumn(
                name: "Exercise_Id",
                table: "WorkoutData",
                newName: "ExerciseId");

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

            migrationBuilder.CreateIndex(
                name: "IX_SegmentData_TrainingProgramId",
                table: "SegmentData",
                column: "TrainingProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDayData_ProgramSegmentId",
                table: "ProgramDayData",
                column: "ProgramSegmentId");

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
        }
    }
}
