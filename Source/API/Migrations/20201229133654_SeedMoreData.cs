using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class SeedMoreData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "Email", "Salt" },
                values: new object[] { "test1@test.com", new byte[] { 98, 242, 193, 225, 215, 239, 23, 188, 144, 175, 97, 36, 49, 221, 171, 186 } });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "Email", "Salt" },
                values: new object[] { "test2@test.com", new byte[] { 161, 204, 146, 147, 219, 192, 115, 82, 46, 32, 157, 129, 136, 184, 104, 238 } });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "Email", "Salt" },
                values: new object[] { "test3@test.com", new byte[] { 45, 150, 38, 1, 127, 253, 85, 87, 29, 12, 19, 194, 214, 13, 53, 255 } });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "Email", "Salt" },
                values: new object[] { "test4@test.com", new byte[] { 151, 172, 85, 144, 23, 23, 173, 80, 121, 237, 39, 238, 186, 9, 248, 177 } });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "Email", "Salt" },
                values: new object[] { "test5@test.com", new byte[] { 17, 90, 218, 23, 39, 171, 16, 249, 5, 211, 26, 138, 64, 150, 254, 125 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "Email", "Salt" },
                values: new object[] { "test@test.com", new byte[] { 187, 146, 218, 185, 61, 152, 74, 200, 150, 34, 131, 60, 237, 76, 43, 233 } });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "Email", "Salt" },
                values: new object[] { "test@test.com", new byte[] { 170, 210, 179, 143, 114, 144, 6, 176, 133, 81, 138, 238, 137, 248, 170, 81 } });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "Email", "Salt" },
                values: new object[] { "test@test.com", new byte[] { 28, 97, 96, 195, 186, 83, 40, 170, 230, 148, 12, 214, 24, 42, 146, 7 } });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "Email", "Salt" },
                values: new object[] { "test@test.com", new byte[] { 111, 144, 233, 113, 240, 124, 14, 15, 238, 190, 30, 163, 76, 95, 152, 70 } });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "Email", "Salt" },
                values: new object[] { "test@test.com", new byte[] { 194, 89, 251, 187, 235, 243, 143, 182, 180, 76, 249, 56, 154, 174, 184, 41 } });
        }
    }
}
