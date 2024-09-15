using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class DeletingCorrectChoices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Frameworks_AspNetUsers_ApplicationUserId",
                table: "Frameworks");

            migrationBuilder.DropIndex(
                name: "IX_Frameworks_ApplicationUserId",
                table: "Frameworks");

            migrationBuilder.DropColumn(
                name: "CorrectChoices",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Frameworks");

            migrationBuilder.CreateTable(
                name: "ApplicationUserFramework",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FrameworksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserFramework", x => new { x.ApplicationUserId, x.FrameworksId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserFramework_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserFramework_Frameworks_FrameworksId",
                        column: x => x.FrameworksId,
                        principalTable: "Frameworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserFramework_FrameworksId",
                table: "ApplicationUserFramework",
                column: "FrameworksId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserFramework");

            migrationBuilder.AddColumn<string>(
                name: "CorrectChoices",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Frameworks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Frameworks_ApplicationUserId",
                table: "Frameworks",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Frameworks_AspNetUsers_ApplicationUserId",
                table: "Frameworks",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
