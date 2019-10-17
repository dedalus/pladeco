using Microsoft.EntityFrameworkCore.Migrations;

namespace Pladeco.Web.Migrations
{
    public partial class RemoveNullableFromProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "SectorID",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ResponsableUnitID",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DevAxisID",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

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

            migrationBuilder.AlterColumn<int>(
                name: "SectorID",
                table: "Projects",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ResponsableUnitID",
                table: "Projects",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "DevAxisID",
                table: "Projects",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_DevAxes_DevAxisID",
                table: "Projects",
                column: "DevAxisID",
                principalTable: "DevAxes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ResponsableUnits_ResponsableUnitID",
                table: "Projects",
                column: "ResponsableUnitID",
                principalTable: "ResponsableUnits",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Sectors_SectorID",
                table: "Projects",
                column: "SectorID",
                principalTable: "Sectors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
