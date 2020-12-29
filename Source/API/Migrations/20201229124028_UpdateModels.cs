using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class UpdateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Salt",
                table: "User",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SellerPage",
                type: "VARCHAR(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SellerPage",
                keyColumn: "SellerPageID",
                keyValue: 1,
                column: "Description",
                value: "Här på Jannes gård säljer vi dem färskaste varorna i hela Västra Götaland!");

            migrationBuilder.UpdateData(
                table: "SellerPage",
                keyColumn: "SellerPageID",
                keyValue: 2,
                column: "Description",
                value: "Lisas Näroldat: Bättre grönsaker finns inte!");

            migrationBuilder.UpdateData(
                table: "SellerPage",
                keyColumn: "SellerPageID",
                keyValue: 3,
                column: "Description",
                value: "Vi säljer dem bästa varorna i hela Göteborg!");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserID",
                keyValue: 1,
                column: "Salt",
                value: new byte[] { 187, 146, 218, 185, 61, 152, 74, 200, 150, 34, 131, 60, 237, 76, 43, 233 });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserID",
                keyValue: 2,
                column: "Salt",
                value: new byte[] { 170, 210, 179, 143, 114, 144, 6, 176, 133, 81, 138, 238, 137, 248, 170, 81 });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserID",
                keyValue: 3,
                column: "Salt",
                value: new byte[] { 28, 97, 96, 195, 186, 83, 40, 170, 230, 148, 12, 214, 24, 42, 146, 7 });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserID",
                keyValue: 4,
                column: "Salt",
                value: new byte[] { 111, 144, 233, 113, 240, 124, 14, 15, 238, 190, 30, 163, 76, 95, 152, 70 });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserID",
                keyValue: 5,
                column: "Salt",
                value: new byte[] { 194, 89, 251, 187, 235, 243, 143, 182, 180, 76, 249, 56, 154, 174, 184, 41 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "SellerPage");
        }
    }
}
