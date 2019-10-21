using Microsoft.EntityFrameworkCore.Migrations;

namespace Pladeco.Web.Migrations
{
    public partial class AddStagesToModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StageID",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_StageID",
                table: "Projects",
                column: "StageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_TypologyStages_StageID",
                table: "Projects",
                column: "StageID",
                principalTable: "TypologyStages",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_TypologyStages_StageID",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_StageID",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "StageID",
                table: "Projects");
        }
    }
}
