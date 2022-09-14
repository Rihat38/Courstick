using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Courstick.Infrastructure.Migrations
{
    public partial class AddingCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Statuses_StatusId",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Courses",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "Courses",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Statuses_StatusId",
                table: "Courses",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Statuses_StatusId",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Courses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "Courses",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Statuses_StatusId",
                table: "Courses",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
