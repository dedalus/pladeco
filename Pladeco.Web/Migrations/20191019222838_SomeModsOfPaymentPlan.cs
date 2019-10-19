using Microsoft.EntityFrameworkCore.Migrations;

namespace Pladeco.Web.Migrations
{
    public partial class SomeModsOfPaymentPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPlans_Projects_ProjectID",
                table: "PaymentPlans");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectID",
                table: "PaymentPlans",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPlans_Projects_ProjectID",
                table: "PaymentPlans",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPlans_Projects_ProjectID",
                table: "PaymentPlans");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectID",
                table: "PaymentPlans",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPlans_Projects_ProjectID",
                table: "PaymentPlans",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
