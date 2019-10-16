using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pladeco.Web.Migrations
{
    public partial class SomeNewModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Sector_SectorID",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sector",
                table: "Sector");

            migrationBuilder.RenameTable(
                name: "Sector",
                newName: "Sectors");

            migrationBuilder.AddColumn<int>(
                name: "DevAxisID",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResponsableUnitID",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sectors",
                table: "Sectors",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "DevAxes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    create_date = table.Column<DateTime>(nullable: true),
                    create_uid = table.Column<int>(nullable: true),
                    write_date = table.Column<DateTime>(nullable: true),
                    write_uid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevAxes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ResponsableUnits",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    create_date = table.Column<DateTime>(nullable: true),
                    create_uid = table.Column<int>(nullable: true),
                    write_date = table.Column<DateTime>(nullable: true),
                    write_uid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponsableUnits", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DevAxisID",
                table: "Projects",
                column: "DevAxisID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ResponsableUnitID",
                table: "Projects",
                column: "ResponsableUnitID");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_DevAxes_DevAxisID",
                table: "Projects",
                column: "DevAxisID",
                principalTable: "DevAxes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ResponsableUnits_ResponsableUnitID",
                table: "Projects",
                column: "ResponsableUnitID",
                principalTable: "ResponsableUnits",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Sectors_SectorID",
                table: "Projects",
                column: "SectorID",
                principalTable: "Sectors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_DevAxes_DevAxisID",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ResponsableUnits_ResponsableUnitID",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Sectors_SectorID",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "DevAxes");

            migrationBuilder.DropTable(
                name: "ResponsableUnits");

            migrationBuilder.DropIndex(
                name: "IX_Projects_DevAxisID",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ResponsableUnitID",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sectors",
                table: "Sectors");

            migrationBuilder.DropColumn(
                name: "DevAxisID",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ResponsableUnitID",
                table: "Projects");

            migrationBuilder.RenameTable(
                name: "Sectors",
                newName: "Sector");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sector",
                table: "Sector",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Sector_SectorID",
                table: "Projects",
                column: "SectorID",
                principalTable: "Sector",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
