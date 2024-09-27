using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Reportsandotherstuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Questions_QuestionId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_QuestionId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_TestId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "Feedback",
                table: "Reports",
                newName: "AdminId");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewedAt",
                table: "Reports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "QuestionAssessments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PointsAwarded = table.Column<int>(type: "int", nullable: false),
                    ReportId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAssessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionAssessments_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionAssessments_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_TestId",
                table: "Reports",
                column: "TestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAssessments_QuestionId",
                table: "QuestionAssessments",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAssessments_ReportId",
                table: "QuestionAssessments",
                column: "ReportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionAssessments");

            migrationBuilder.DropIndex(
                name: "IX_Reports_TestId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ReviewedAt",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "AdminId",
                table: "Reports",
                newName: "Feedback");

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "Reports",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_QuestionId",
                table: "Reports",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_TestId",
                table: "Reports",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Questions_QuestionId",
                table: "Reports",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
