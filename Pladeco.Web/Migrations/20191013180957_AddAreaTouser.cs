using Microsoft.EntityFrameworkCore.Migrations;

namespace Pladeco.Web.Migrations
{
    public partial class AddAreaTouser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AreaID",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AreaID",
                table: "Users",
                column: "AreaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Areas_AreaID",
                table: "Users",
                column: "AreaID",
                principalTable: "Areas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Areas_AreaID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AreaID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AreaID",
                table: "Users");
        }
    }
}
