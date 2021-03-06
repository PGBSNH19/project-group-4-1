﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Unique_username_and_email : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Marketplaces",
                columns: table => new
                {
                    MarketplaceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictureBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marketplaces", x => x.MarketplaceID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictureBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "MarketplaceSellers",
                columns: table => new
                {
                    MarketplaceID = table.Column<int>(type: "int", nullable: false),
                    SellerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketplaceSellers", x => new { x.MarketplaceID, x.SellerID });
                    table.ForeignKey(
                        name: "FK_MarketplaceSellers_Marketplaces_MarketplaceID",
                        column: x => x.MarketplaceID,
                        principalTable: "Marketplaces",
                        principalColumn: "MarketplaceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarketplaceSellers_Users_SellerID",
                        column: x => x.SellerID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "SellerPages",
                columns: table => new
                {
                    SellerPageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerUserID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerPages", x => x.SellerPageID);
                    table.ForeignKey(
                        name: "FK_SellerPages_Users_SellerUserID",
                        column: x => x.SellerUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProducts",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProducts", x => new { x.UserID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_UserProducts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK_UserProducts_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "SellerPageProducts",
                columns: table => new
                {
                    SellerPageID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerPageProducts", x => new { x.ProductID, x.SellerPageID });
                    table.ForeignKey(
                        name: "FK_SellerPageProducts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK_SellerPageProducts_SellerPages_SellerPageID",
                        column: x => x.SellerPageID,
                        principalTable: "SellerPages",
                        principalColumn: "SellerPageID");
                });

            migrationBuilder.InsertData(
                table: "Marketplaces",
                columns: new[] { "MarketplaceID", "EndDateTime", "Location", "Name", "PictureBytes", "StartDateTime" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 12, 26, 16, 30, 0, 0, DateTimeKind.Unspecified), "Heden", "Göteborgs Bakluckeloppis", null, new DateTime(2020, 12, 26, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2020, 12, 30, 16, 30, 0, 0, DateTimeKind.Unspecified), "Majorna", "Majornas Eko-Marknad", null, new DateTime(2020, 12, 30, 9, 30, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Name", "PictureBytes" },
                values: new object[,]
                {
                    { 1, "Potatis", null },
                    { 2, "Morot", null },
                    { 3, "Äpplen", null },
                    { 4, "Päron", null },
                    { 5, "Nöttfärs", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Email", "Password", "Salt", "Type", "Username" },
                values: new object[,]
                {
                    { 1, "test@test.com", "lösen123", null, 1, "JanneBonde07" },
                    { 2, "test1@test.com", "lösen123", null, 0, "Bengtan555" },
                    { 3, "test2@test.com", "KlDioL123!", null, 0, "Henrik123" },
                    { 4, "test3@test.com", "lösen123", null, 1, "BondenLisa1" },
                    { 5, "test4@test.com", "lösen123", null, 1, "HannesFarm" }
                });

            migrationBuilder.InsertData(
                table: "MarketplaceSellers",
                columns: new[] { "MarketplaceID", "SellerID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 3 },
                    { 2, 5 }
                });

            migrationBuilder.InsertData(
                table: "SellerPages",
                columns: new[] { "SellerPageID", "Description", "Name", "SellerUserID" },
                values: new object[,]
                {
                    { 1, null, "Jannes Online-Gård", 1 },
                    { 2, null, "Lisas Näroldat", 4 },
                    { 3, null, "Hannes eko-farm", 4 }
                });

            migrationBuilder.InsertData(
                table: "UserProducts",
                columns: new[] { "ProductID", "UserID" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 5, 3 }
                });

            migrationBuilder.InsertData(
                table: "SellerPageProducts",
                columns: new[] { "ProductID", "SellerPageID", "Price", "Stock" },
                values: new object[] { 1, 1, 2, 10 });

            migrationBuilder.InsertData(
                table: "SellerPageProducts",
                columns: new[] { "ProductID", "SellerPageID", "Price", "Stock" },
                values: new object[] { 5, 2, 55, 25 });

            migrationBuilder.InsertData(
                table: "SellerPageProducts",
                columns: new[] { "ProductID", "SellerPageID", "Price", "Stock" },
                values: new object[] { 3, 3, 10, 100 });

            migrationBuilder.CreateIndex(
                name: "IX_MarketplaceSellers_SellerID",
                table: "MarketplaceSellers",
                column: "SellerID");

            migrationBuilder.CreateIndex(
                name: "IX_SellerPageProducts_SellerPageID",
                table: "SellerPageProducts",
                column: "SellerPageID");

            migrationBuilder.CreateIndex(
                name: "IX_SellerPages_SellerUserID",
                table: "SellerPages",
                column: "SellerUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserProducts_ProductID",
                table: "UserProducts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarketplaceSellers");

            migrationBuilder.DropTable(
                name: "SellerPageProducts");

            migrationBuilder.DropTable(
                name: "UserProducts");

            migrationBuilder.DropTable(
                name: "Marketplaces");

            migrationBuilder.DropTable(
                name: "SellerPages");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
