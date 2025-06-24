using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAOS_API.Migrations
{
    /// <inheritdoc />
    public partial class ProjectRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProgramData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Subtitle = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramData", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SegmentData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Subtitle = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TrainingProgramId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SegmentData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SegmentData_ProgramData_TrainingProgramId",
                        column: x => x.TrainingProgramId,
                        principalTable: "ProgramData",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProgramDayData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WeekNum = table.Column<int>(type: "int", nullable: false),
                    ProgramSegmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramDayData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramDayData_SegmentData_ProgramSegmentId",
                        column: x => x.ProgramSegmentId,
                        principalTable: "SegmentData",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CircuitData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProgramDayId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CircuitData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CircuitData_ProgramDayData_ProgramDayId",
                        column: x => x.ProgramDayId,
                        principalTable: "ProgramDayData",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkoutData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Label = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RecommendedSets = table.Column<int>(type: "int", nullable: false),
                    RecommendedReps = table.Column<int>(type: "int", nullable: false),
                    RecommendedWeight = table.Column<int>(type: "int", nullable: false),
                    CircuitId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutData_CircuitData_CircuitId",
                        column: x => x.CircuitId,
                        principalTable: "CircuitData",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CircuitData_ProgramDayId",
                table: "CircuitData",
                column: "ProgramDayId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDayData_ProgramSegmentId",
                table: "ProgramDayData",
                column: "ProgramSegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SegmentData_TrainingProgramId",
                table: "SegmentData",
                column: "TrainingProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutData_CircuitId",
                table: "WorkoutData",
                column: "CircuitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkoutData");

            migrationBuilder.DropTable(
                name: "CircuitData");

            migrationBuilder.DropTable(
                name: "ProgramDayData");

            migrationBuilder.DropTable(
                name: "SegmentData");

            migrationBuilder.DropTable(
                name: "ProgramData");
        }
    }
}
