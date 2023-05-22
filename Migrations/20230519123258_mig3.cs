using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebFrontToBack.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkImages_Services_ServiceId",
                table: "WorkImages");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkImages_Works_WorkId",
                table: "WorkImages");

            migrationBuilder.DropIndex(
                name: "IX_WorkImages_ServiceId",
                table: "WorkImages");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "WorkImages");

            migrationBuilder.AlterColumn<int>(
                name: "WorkId",
                table: "WorkImages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkImages_Works_WorkId",
                table: "WorkImages",
                column: "WorkId",
                principalTable: "Works",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkImages_Works_WorkId",
                table: "WorkImages");

            migrationBuilder.AlterColumn<int>(
                name: "WorkId",
                table: "WorkImages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "WorkImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WorkImages_ServiceId",
                table: "WorkImages",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkImages_Services_ServiceId",
                table: "WorkImages",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkImages_Works_WorkId",
                table: "WorkImages",
                column: "WorkId",
                principalTable: "Works",
                principalColumn: "Id");
        }
    }
}
