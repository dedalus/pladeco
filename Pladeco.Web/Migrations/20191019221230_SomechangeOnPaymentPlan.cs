using Microsoft.EntityFrameworkCore.Migrations;

namespace Pladeco.Web.Migrations
{
    public partial class SomechangeOnPaymentPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPlans_Users_SolicitanteId",
                table: "PaymentPlans");

            migrationBuilder.RenameColumn(
                name: "SolicitanteId",
                table: "PaymentPlans",
                newName: "SolicitanteID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentPlans_SolicitanteId",
                table: "PaymentPlans",
                newName: "IX_PaymentPlans_SolicitanteID");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PaymentPlans",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPlans_Users_SolicitanteID",
                table: "PaymentPlans",
                column: "SolicitanteID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPlans_Users_SolicitanteID",
                table: "PaymentPlans");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PaymentPlans");

            migrationBuilder.RenameColumn(
                name: "SolicitanteID",
                table: "PaymentPlans",
                newName: "SolicitanteId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentPlans_SolicitanteID",
                table: "PaymentPlans",
                newName: "IX_PaymentPlans_SolicitanteId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPlans_Users_SolicitanteId",
                table: "PaymentPlans",
                column: "SolicitanteId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
