using Microsoft.EntityFrameworkCore.Migrations;

namespace Pladeco.Web.Migrations
{
    public partial class SomeFieldsToProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Budget",
                table: "Areas");

            migrationBuilder.AddColumn<string>(
                name: "ComplianceIndicator",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StrategyTarget",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationUnit",
                table: "Projects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComplianceIndicator",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "StrategyTarget",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "VerificationUnit",
                table: "Projects");

            migrationBuilder.AddColumn<decimal>(
                name: "Budget",
                table: "Areas",
                nullable: true);
        }
    }
}
