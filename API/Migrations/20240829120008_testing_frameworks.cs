using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class testing_frameworks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Frameworks_FrameworkId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FrameworkId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FrameworkId",
                table: "AspNetUsers");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Frameworks_AspNetUsers_ApplicationUserId",
                table: "Frameworks");

            migrationBuilder.DropIndex(
                name: "IX_Frameworks_ApplicationUserId",
                table: "Frameworks");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Frameworks");

            migrationBuilder.AddColumn<int>(
                name: "FrameworkId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FrameworkId",
                table: "AspNetUsers",
                column: "FrameworkId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Frameworks_FrameworkId",
                table: "AspNetUsers",
                column: "FrameworkId",
                principalTable: "Frameworks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
