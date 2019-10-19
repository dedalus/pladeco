using Microsoft.EntityFrameworkCore.Migrations;

namespace Pladeco.Web.Migrations
{
    public partial class AddResponsableToPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Users_ResponsableId",
                table: "Plans");

            migrationBuilder.RenameColumn(
                name: "ResponsableId",
                table: "Plans",
                newName: "ResponsableID");

            migrationBuilder.RenameIndex(
                name: "IX_Plans_ResponsableId",
                table: "Plans",
                newName: "IX_Plans_ResponsableID");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Users_ResponsableID",
                table: "Plans",
                column: "ResponsableID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Users_ResponsableID",
                table: "Plans");

            migrationBuilder.RenameColumn(
                name: "ResponsableID",
                table: "Plans",
                newName: "ResponsableId");

            migrationBuilder.RenameIndex(
                name: "IX_Plans_ResponsableID",
                table: "Plans",
                newName: "IX_Plans_ResponsableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Users_ResponsableId",
                table: "Plans",
                column: "ResponsableId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
