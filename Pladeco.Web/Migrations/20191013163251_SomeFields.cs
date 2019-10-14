using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pladeco.Web.Migrations
{
    public partial class SomeFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "create_date",
                table: "Areas",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "create_uid",
                table: "Areas",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "write_date",
                table: "Areas",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "write_uid",
                table: "Areas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "create_date",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "create_uid",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "write_date",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "write_uid",
                table: "Areas");
        }
    }
}
