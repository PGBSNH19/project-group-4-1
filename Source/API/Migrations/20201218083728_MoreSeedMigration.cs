using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class MoreSeedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Marketplace",
                columns: new[] { "MarketplaceID", "EndDateTime", "Location", "Name", "StartDateTime" },
                values: new object[] { 2, new DateTime(2020, 12, 30, 16, 30, 0, 0, DateTimeKind.Unspecified), "Majorna", "Majornas Eko-Marknad", new DateTime(2020, 12, 30, 9, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Name", "SellerPageID" },
                values: new object[,]
                {
                    { 2, "Morot", null },
                    { 3, "Äpplen", null },
                    { 4, "Päron", null },
                    { 5, "Nöttfärs", null }
                });

            migrationBuilder.UpdateData(
                table: "SellerPage",
                keyColumn: "SellerPageID",
                keyValue: 1,
                columns: new[] { "Name", "SellerUserID" },
                values: new object[] { "Jannes Online-Gård", 1 });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserID",
                keyValue: 1,
                column: "Email",
                value: "test@test.com");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserID",
                keyValue: 2,
                column: "Email",
                value: "test@test.com");

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserID", "Email", "Password", "Type", "Username" },
                values: new object[,]
                {
                    { 3, "test@test.com", "KlDioL123!", 1, "Henrik123" },
                    { 4, "test@test.com", "lösen123", 2, "BondenLisa1" },
                    { 5, "test@test.com", "lösen123", 2, "HannesFarm" }
                });

            migrationBuilder.InsertData(
                table: "MarketplaceSeller",
                columns: new[] { "MarketplaceID", "SellerID" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 2, 5 }
                });

            migrationBuilder.InsertData(
                table: "SellerPage",
                columns: new[] { "SellerPageID", "Name", "SellerUserID" },
                values: new object[,]
                {
                    { 2, "Lisas Näroldat", 4 },
                    { 3, "Hannes eko-farm", 4 }
                });

            migrationBuilder.InsertData(
                table: "UserProduct",
                columns: new[] { "ProductID", "UserID" },
                values: new object[] { 5, 3 });

            migrationBuilder.InsertData(
                table: "SellerPageProduct",
                columns: new[] { "ProductID", "SellerPageID", "Price", "Stock" },
                values: new object[] { 5, 2, 55, 25 });

            migrationBuilder.InsertData(
                table: "SellerPageProduct",
                columns: new[] { "ProductID", "SellerPageID", "Price", "Stock" },
                values: new object[] { 3, 3, 10, 100 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MarketplaceSeller",
                keyColumns: new[] { "MarketplaceID", "SellerID" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "MarketplaceSeller",
                keyColumns: new[] { "MarketplaceID", "SellerID" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SellerPageProduct",
                keyColumns: new[] { "ProductID", "SellerPageID" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "SellerPageProduct",
                keyColumns: new[] { "ProductID", "SellerPageID" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "UserProduct",
                keyColumns: new[] { "ProductID", "UserID" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "Marketplace",
                keyColumn: "MarketplaceID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SellerPage",
                keyColumn: "SellerPageID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SellerPage",
                keyColumn: "SellerPageID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserID",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "Email",
                table: "User");

            migrationBuilder.UpdateData(
                table: "SellerPage",
                keyColumn: "SellerPageID",
                keyValue: 1,
                columns: new[] { "Name", "SellerUserID" },
                values: new object[] { "Arnes Online-Gård", 2 });
        }
    }
}
