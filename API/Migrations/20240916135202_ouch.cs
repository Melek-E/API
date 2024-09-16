using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class ouch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserFramework_AspNetUsers_UsersId",
                table: "ApplicationUserFramework");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserFramework",
                table: "ApplicationUserFramework");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserFramework_UsersId",
                table: "ApplicationUserFramework");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "ApplicationUserFramework",
                newName: "ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserFramework",
                table: "ApplicationUserFramework",
                columns: new[] { "ApplicationUserId", "FrameworksId" });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserFramework_FrameworksId",
                table: "ApplicationUserFramework",
                column: "FrameworksId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserFramework_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserFramework",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserFramework_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserFramework");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserFramework",
                table: "ApplicationUserFramework");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserFramework_FrameworksId",
                table: "ApplicationUserFramework");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "ApplicationUserFramework",
                newName: "UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserFramework",
                table: "ApplicationUserFramework",
                columns: new[] { "FrameworksId", "UsersId" });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserFramework_UsersId",
                table: "ApplicationUserFramework",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserFramework_AspNetUsers_UsersId",
                table: "ApplicationUserFramework",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
