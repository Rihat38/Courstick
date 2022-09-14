using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Courstick.Infrastructure.Migrations
{
    public partial class AuthV14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "4642e85f-9114-4fef-a502-636bc9b651af", "DEFAULTUSER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "d0876391-25e9-43fc-a25b-376b37d0bf33", null });
        }
    }
}
