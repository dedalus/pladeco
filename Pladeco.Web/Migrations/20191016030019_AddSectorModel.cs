using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pladeco.Web.Migrations
{
    public partial class AddSectorModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SectorID",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Sector",
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
                    table.PrimaryKey("PK_Sector", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_SectorID",
                table: "Projects",
                column: "SectorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Sector_SectorID",
                table: "Projects",
                column: "SectorID",
                principalTable: "Sector",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Sector_SectorID",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "Sector");

            migrationBuilder.DropIndex(
                name: "IX_Projects_SectorID",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "SectorID",
                table: "Projects");
        }
    }
}
