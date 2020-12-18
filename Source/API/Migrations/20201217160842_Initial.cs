using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Marketplace",
                columns: table => new
                {
                    MarketplaceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marketplace", x => x.MarketplaceID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "MarketplaceSeller",
                columns: table => new
                {
                    MarketplaceID = table.Column<int>(type: "int", nullable: false),
                    SellerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketplaceSeller", x => new { x.MarketplaceID, x.SellerID });
                    table.ForeignKey(
                        name: "FK_MarketplaceSeller_Marketplace_MarketplaceID",
                        column: x => x.MarketplaceID,
                        principalTable: "Marketplace",
                        principalColumn: "MarketplaceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarketplaceSeller_User_SellerID",
                        column: x => x.SellerID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "SellerPage",
                columns: table => new
                {
                    SellerPageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerUserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerPage", x => x.SellerPageID);
                    table.ForeignKey(
                        name: "FK_SellerPage_User_SellerUserID",
                        column: x => x.SellerUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerPageID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Products_SellerPage_SellerPageID",
                        column: x => x.SellerPageID,
                        principalTable: "SellerPage",
                        principalColumn: "SellerPageID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SellerPageProduct",
                columns: table => new
                {
                    SellerPageID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerPageProduct", x => new { x.ProductID, x.SellerPageID });
                    table.ForeignKey(
                        name: "FK_SellerPageProduct_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK_SellerPageProduct_SellerPage_SellerPageID",
                        column: x => x.SellerPageID,
                        principalTable: "SellerPage",
                        principalColumn: "SellerPageID");
                });

            migrationBuilder.CreateTable(
                name: "UserProduct",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProduct", x => new { x.UserID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_UserProduct_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK_UserProduct_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.InsertData(
                table: "Marketplace",
                columns: new[] { "MarketplaceID", "EndDateTime", "Location", "Name", "StartDateTime" },
                values: new object[] { 1, new DateTime(2020, 12, 26, 16, 30, 0, 0, DateTimeKind.Unspecified), "Heden", "Göteborgs Bakluckeloppis", new DateTime(2020, 12, 26, 10, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Name", "SellerPageID" },
                values: new object[] { 1, "Potatis", null });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserID", "Password", "Type", "Username" },
                values: new object[,]
                {
                    { 1, "lösen123", 2, "JanneBonde07" },
                    { 2, "lösen123", 1, "Bengtan555" }
                });

            migrationBuilder.InsertData(
                table: "MarketplaceSeller",
                columns: new[] { "MarketplaceID", "SellerID" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "SellerPage",
                columns: new[] { "SellerPageID", "Name", "SellerUserID" },
                values: new object[] { 1, "Arnes Online-Gård", 2 });

            migrationBuilder.InsertData(
                table: "UserProduct",
                columns: new[] { "ProductID", "UserID" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "SellerPageProduct",
                columns: new[] { "ProductID", "SellerPageID", "Price", "Stock" },
                values: new object[] { 1, 1, 2, 10 });

            migrationBuilder.CreateIndex(
                name: "IX_MarketplaceSeller_SellerID",
                table: "MarketplaceSeller",
                column: "SellerID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SellerPageID",
                table: "Products",
                column: "SellerPageID");

            migrationBuilder.CreateIndex(
                name: "IX_SellerPage_SellerUserID",
                table: "SellerPage",
                column: "SellerUserID");

            migrationBuilder.CreateIndex(
                name: "IX_SellerPageProduct_SellerPageID",
                table: "SellerPageProduct",
                column: "SellerPageID");

            migrationBuilder.CreateIndex(
                name: "IX_UserProduct_ProductID",
                table: "UserProduct",
                column: "ProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarketplaceSeller");

            migrationBuilder.DropTable(
                name: "SellerPageProduct");

            migrationBuilder.DropTable(
                name: "UserProduct");

            migrationBuilder.DropTable(
                name: "Marketplace");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SellerPage");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
