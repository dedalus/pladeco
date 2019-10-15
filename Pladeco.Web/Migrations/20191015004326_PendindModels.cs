using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pladeco.Web.Migrations
{
    public partial class PendindModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "create_date",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "create_uid",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "write_date",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "write_uid",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "create_date",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "create_uid",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "write_date",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "write_uid",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "create_date",
                table: "Plans",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "create_uid",
                table: "Plans",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "write_date",
                table: "Plans",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "write_uid",
                table: "Plans",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "create_date",
                table: "PaymentPlans",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "create_uid",
                table: "PaymentPlans",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "write_date",
                table: "PaymentPlans",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "write_uid",
                table: "PaymentPlans",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "create_date",
                table: "Budgets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "create_uid",
                table: "Budgets",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "write_date",
                table: "Budgets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "write_uid",
                table: "Budgets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "create_date",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "create_uid",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "write_date",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "write_uid",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "create_date",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "create_uid",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "write_date",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "write_uid",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "create_date",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "create_uid",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "write_date",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "write_uid",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "create_date",
                table: "PaymentPlans");

            migrationBuilder.DropColumn(
                name: "create_uid",
                table: "PaymentPlans");

            migrationBuilder.DropColumn(
                name: "write_date",
                table: "PaymentPlans");

            migrationBuilder.DropColumn(
                name: "write_uid",
                table: "PaymentPlans");

            migrationBuilder.DropColumn(
                name: "create_date",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "create_uid",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "write_date",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "write_uid",
                table: "Budgets");
        }
    }
}
