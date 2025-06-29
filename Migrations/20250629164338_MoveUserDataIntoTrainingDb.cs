using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HAOS_API.Migrations
{
    /// <inheritdoc />
    public partial class MoveUserDataIntoTrainingDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserAccountId",
                table: "ProgramData",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccountData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountData", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CompletedWorkoutData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WorkoutId = table.Column<int>(type: "int", nullable: false),
                    UserAccountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedWorkoutData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompletedWorkoutData_AccountData_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "AccountData",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramData_UserAccountId",
                table: "ProgramData",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedWorkoutData_UserAccountId",
                table: "CompletedWorkoutData",
                column: "UserAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramData_AccountData_UserAccountId",
                table: "ProgramData",
                column: "UserAccountId",
                principalTable: "AccountData",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramData_AccountData_UserAccountId",
                table: "ProgramData");

            migrationBuilder.DropTable(
                name: "CompletedWorkoutData");

            migrationBuilder.DropTable(
                name: "AccountData");

            migrationBuilder.DropIndex(
                name: "IX_ProgramData_UserAccountId",
                table: "ProgramData");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "ProgramData");
        }
    }
}
