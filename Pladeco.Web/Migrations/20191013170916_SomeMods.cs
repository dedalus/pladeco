using Microsoft.EntityFrameworkCore.Migrations;

namespace Pladeco.Web.Migrations
{
    public partial class SomeMods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Projects_ProjectID",
                table: "Plans");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Areas_AreaID",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_ResponsableId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_SolicitanteId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "SolicitanteId",
                table: "Projects",
                newName: "SolicitanteID");

            migrationBuilder.RenameColumn(
                name: "ResponsableId",
                table: "Projects",
                newName: "ResponsableID");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_SolicitanteId",
                table: "Projects",
                newName: "IX_Projects_SolicitanteID");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_ResponsableId",
                table: "Projects",
                newName: "IX_Projects_ResponsableID");

            migrationBuilder.AlterColumn<int>(
                name: "AreaID",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectID",
                table: "Plans",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Projects_ProjectID",
                table: "Plans",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Areas_AreaID",
                table: "Projects",
                column: "AreaID",
                principalTable: "Areas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_ResponsableID",
                table: "Projects",
                column: "ResponsableID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_SolicitanteID",
                table: "Projects",
                column: "SolicitanteID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Projects_ProjectID",
                table: "Plans");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Areas_AreaID",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_ResponsableID",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_SolicitanteID",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "SolicitanteID",
                table: "Projects",
                newName: "SolicitanteId");

            migrationBuilder.RenameColumn(
                name: "ResponsableID",
                table: "Projects",
                newName: "ResponsableId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_SolicitanteID",
                table: "Projects",
                newName: "IX_Projects_SolicitanteId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_ResponsableID",
                table: "Projects",
                newName: "IX_Projects_ResponsableId");

            migrationBuilder.AlterColumn<int>(
                name: "AreaID",
                table: "Projects",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ProjectID",
                table: "Plans",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Projects_ProjectID",
                table: "Plans",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Areas_AreaID",
                table: "Projects",
                column: "AreaID",
                principalTable: "Areas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_ResponsableId",
                table: "Projects",
                column: "ResponsableId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_SolicitanteId",
                table: "Projects",
                column: "SolicitanteId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
