using Microsoft.EntityFrameworkCore.Migrations;

namespace Pladeco.Web.Migrations
{
    public partial class SomeFKTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Areas_AreaID",
                table: "Budgets");

            migrationBuilder.AlterColumn<int>(
                name: "AreaID",
                table: "Budgets",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Areas_AreaID",
                table: "Budgets",
                column: "AreaID",
                principalTable: "Areas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Areas_AreaID",
                table: "Budgets");

            migrationBuilder.AlterColumn<int>(
                name: "AreaID",
                table: "Budgets",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Areas_AreaID",
                table: "Budgets",
                column: "AreaID",
                principalTable: "Areas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
