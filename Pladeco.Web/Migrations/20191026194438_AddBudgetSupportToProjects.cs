using Microsoft.EntityFrameworkCore.Migrations;

namespace Pladeco.Web.Migrations
{
    public partial class AddBudgetSupportToProjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BudgetAmount",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BudgetDescription",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponsableBudgetID",
                table: "Projects",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ResponsableBudgetID",
                table: "Projects",
                column: "ResponsableBudgetID");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_ResponsableBudgetID",
                table: "Projects",
                column: "ResponsableBudgetID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_ResponsableBudgetID",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ResponsableBudgetID",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "BudgetAmount",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "BudgetDescription",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ResponsableBudgetID",
                table: "Projects");
        }
    }
}
