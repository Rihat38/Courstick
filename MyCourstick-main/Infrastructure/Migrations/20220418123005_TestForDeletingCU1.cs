using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Courstick.Infrastructure.Migrations
{
    public partial class TestForDeletingCU1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseUser_AspNetUsers_AuthorId",
                table: "CourseUser");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseUser_Courses_AuthorOfCourseId",
                table: "CourseUser");

            migrationBuilder.DropTable(
                name: "CourseUser1");

            migrationBuilder.RenameColumn(
                name: "AuthorOfCourseId",
                table: "CourseUser",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "CourseUser",
                newName: "CoursesCourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseUser_AuthorOfCourseId",
                table: "CourseUser",
                newName: "IX_CourseUser_UsersId");

            migrationBuilder.CreateTable(
                name: "AuthorOfCourse",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    AuthorOfCourseId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorOfCourse", x => new { x.AuthorId, x.AuthorOfCourseId });
                    table.ForeignKey(
                        name: "FK_AuthorOfCourse_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorOfCourse_Courses_AuthorOfCourseId",
                        column: x => x.AuthorOfCourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorOfCourse_AuthorOfCourseId",
                table: "AuthorOfCourse",
                column: "AuthorOfCourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseUser_AspNetUsers_UsersId",
                table: "CourseUser",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseUser_Courses_CoursesCourseId",
                table: "CourseUser",
                column: "CoursesCourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseUser_AspNetUsers_UsersId",
                table: "CourseUser");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseUser_Courses_CoursesCourseId",
                table: "CourseUser");

            migrationBuilder.DropTable(
                name: "AuthorOfCourse");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "CourseUser",
                newName: "AuthorOfCourseId");

            migrationBuilder.RenameColumn(
                name: "CoursesCourseId",
                table: "CourseUser",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseUser_UsersId",
                table: "CourseUser",
                newName: "IX_CourseUser_AuthorOfCourseId");

            migrationBuilder.CreateTable(
                name: "CourseUser1",
                columns: table => new
                {
                    CoursesCourseId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseUser1", x => new { x.CoursesCourseId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_CourseUser1_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseUser1_Courses_CoursesCourseId",
                        column: x => x.CoursesCourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseUser1_UsersId",
                table: "CourseUser1",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseUser_AspNetUsers_AuthorId",
                table: "CourseUser",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseUser_Courses_AuthorOfCourseId",
                table: "CourseUser",
                column: "AuthorOfCourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
