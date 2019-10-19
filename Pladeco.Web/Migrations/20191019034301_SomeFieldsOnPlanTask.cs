using Microsoft.EntityFrameworkCore.Migrations;

namespace Pladeco.Web.Migrations
{
    public partial class SomeFieldsOnPlanTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Plans_PlanID",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "PlanID",
                table: "Tasks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Tasks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ResponsableID",
                table: "Tasks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ResponsableID",
                table: "Tasks",
                column: "ResponsableID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Plans_PlanID",
                table: "Tasks",
                column: "PlanID",
                principalTable: "Plans",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_ResponsableID",
                table: "Tasks",
                column: "ResponsableID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Plans_PlanID",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_ResponsableID",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ResponsableID",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ResponsableID",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "PlanID",
                table: "Tasks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Plans_PlanID",
                table: "Tasks",
                column: "PlanID",
                principalTable: "Plans",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
