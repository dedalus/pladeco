using Microsoft.EntityFrameworkCore.Migrations;

namespace Pladeco.Web.Migrations
{
    public partial class AddTypologyRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ResponsableID",
                table: "Tasks",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypologyID",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ResponsableID",
                table: "Plans",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_TypologyID",
                table: "Projects",
                column: "TypologyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Typologies_TypologyID",
                table: "Projects",
                column: "TypologyID",
                principalTable: "Typologies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Typologies_TypologyID",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_TypologyID",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TypologyID",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "ResponsableID",
                table: "Tasks",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ResponsableID",
                table: "Plans",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
