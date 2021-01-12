using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Unique_username_and_email : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarketplaceSellers_Marketplaces_MarketplaceID",
                table: "MarketplaceSellers");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketplaceSellers_Users_SellerID",
                table: "MarketplaceSellers");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_SellerPages_SellerPageID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_SellerPageProducts_Products_ProductID",
                table: "SellerPageProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SellerPageProducts_SellerPages_SellerPageID",
                table: "SellerPageProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SellerPages_Users_SellerUserID",
                table: "SellerPages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProducts_Products_ProductID",
                table: "UserProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProducts_Users_UserID",
                table: "UserProducts");

            migrationBuilder.DropIndex(
                name: "IX_Products_SellerPageID",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProducts",
                table: "UserProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SellerPages",
                table: "SellerPages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SellerPageProducts",
                table: "SellerPageProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarketplaceSellers",
                table: "MarketplaceSellers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marketplaces",
                table: "Marketplaces");

            migrationBuilder.DropColumn(
                name: "SellerPageID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "UserProducts");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "UserProducts",
                newName: "UserProduct");

            migrationBuilder.RenameTable(
                name: "SellerPages",
                newName: "SellerPage");

            migrationBuilder.RenameTable(
                name: "SellerPageProducts",
                newName: "SellerPageProduct");

            migrationBuilder.RenameTable(
                name: "MarketplaceSellers",
                newName: "MarketplaceSeller");

            migrationBuilder.RenameTable(
                name: "Marketplaces",
                newName: "Marketplace");

            migrationBuilder.RenameIndex(
                name: "IX_UserProducts_ProductID",
                table: "UserProduct",
                newName: "IX_UserProduct_ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_SellerPages_SellerUserID",
                table: "SellerPage",
                newName: "IX_SellerPage_SellerUserID");

            migrationBuilder.RenameIndex(
                name: "IX_SellerPageProducts_SellerPageID",
                table: "SellerPageProduct",
                newName: "IX_SellerPageProduct_SellerPageID");

            migrationBuilder.RenameIndex(
                name: "IX_MarketplaceSellers_SellerID",
                table: "MarketplaceSeller",
                newName: "IX_MarketplaceSeller_SellerID");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "User",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProduct",
                table: "UserProduct",
                columns: new[] { "UserID", "ProductID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SellerPage",
                table: "SellerPage",
                column: "SellerPageID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SellerPageProduct",
                table: "SellerPageProduct",
                columns: new[] { "ProductID", "SellerPageID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarketplaceSeller",
                table: "MarketplaceSeller",
                columns: new[] { "MarketplaceID", "SellerID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marketplace",
                table: "Marketplace",
                column: "MarketplaceID");

            migrationBuilder.UpdateData(
                table: "SellerPage",
                keyColumn: "SellerPageID",
                keyValue: 1,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "SellerPage",
                keyColumn: "SellerPageID",
                keyValue: 2,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "SellerPage",
                keyColumn: "SellerPageID",
                keyValue: 3,
                column: "Description",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketplaceSeller_Marketplace_MarketplaceID",
                table: "MarketplaceSeller",
                column: "MarketplaceID",
                principalTable: "Marketplace",
                principalColumn: "MarketplaceID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MarketplaceSeller_User_SellerID",
                table: "MarketplaceSeller",
                column: "SellerID",
                principalTable: "User",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_SellerPage_User_SellerUserID",
                table: "SellerPage",
                column: "SellerUserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SellerPageProduct_Products_ProductID",
                table: "SellerPageProduct",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_SellerPageProduct_SellerPage_SellerPageID",
                table: "SellerPageProduct",
                column: "SellerPageID",
                principalTable: "SellerPage",
                principalColumn: "SellerPageID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProduct_Products_ProductID",
                table: "UserProduct",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProduct_User_UserID",
                table: "UserProduct",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarketplaceSeller_Marketplace_MarketplaceID",
                table: "MarketplaceSeller");

            migrationBuilder.DropForeignKey(
                name: "FK_MarketplaceSeller_User_SellerID",
                table: "MarketplaceSeller");

            migrationBuilder.DropForeignKey(
                name: "FK_SellerPage_User_SellerUserID",
                table: "SellerPage");

            migrationBuilder.DropForeignKey(
                name: "FK_SellerPageProduct_Products_ProductID",
                table: "SellerPageProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_SellerPageProduct_SellerPage_SellerPageID",
                table: "SellerPageProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProduct_Products_ProductID",
                table: "UserProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProduct_User_UserID",
                table: "UserProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProduct",
                table: "UserProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_Email",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_Username",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SellerPageProduct",
                table: "SellerPageProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SellerPage",
                table: "SellerPage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarketplaceSeller",
                table: "MarketplaceSeller");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marketplace",
                table: "Marketplace");

            migrationBuilder.RenameTable(
                name: "UserProduct",
                newName: "UserProducts");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "SellerPageProduct",
                newName: "SellerPageProducts");

            migrationBuilder.RenameTable(
                name: "SellerPage",
                newName: "SellerPages");

            migrationBuilder.RenameTable(
                name: "MarketplaceSeller",
                newName: "MarketplaceSellers");

            migrationBuilder.RenameTable(
                name: "Marketplace",
                newName: "Marketplaces");

            migrationBuilder.RenameIndex(
                name: "IX_UserProduct_ProductID",
                table: "UserProducts",
                newName: "IX_UserProducts_ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_SellerPageProduct_SellerPageID",
                table: "SellerPageProducts",
                newName: "IX_SellerPageProducts_SellerPageID");

            migrationBuilder.RenameIndex(
                name: "IX_SellerPage_SellerUserID",
                table: "SellerPages",
                newName: "IX_SellerPages_SellerUserID");

            migrationBuilder.RenameIndex(
                name: "IX_MarketplaceSeller_SellerID",
                table: "MarketplaceSellers",
                newName: "IX_MarketplaceSellers_SellerID");

            migrationBuilder.AddColumn<int>(
                name: "SellerPageID",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "UserProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProducts",
                table: "UserProducts",
                columns: new[] { "UserID", "ProductID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SellerPageProducts",
                table: "SellerPageProducts",
                columns: new[] { "ProductID", "SellerPageID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SellerPages",
                table: "SellerPages",
                column: "SellerPageID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarketplaceSellers",
                table: "MarketplaceSellers",
                columns: new[] { "MarketplaceID", "SellerID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marketplaces",
                table: "Marketplaces",
                column: "MarketplaceID");

            migrationBuilder.UpdateData(
                table: "SellerPages",
                keyColumn: "SellerPageID",
                keyValue: 1,
                column: "Description",
                value: "Här på Jannes gård säljer vi dem färskaste varorna i hela Västra Götaland!");

            migrationBuilder.UpdateData(
                table: "SellerPages",
                keyColumn: "SellerPageID",
                keyValue: 2,
                column: "Description",
                value: "Lisas Näroldat: Bättre grönsaker finns inte!");

            migrationBuilder.UpdateData(
                table: "SellerPages",
                keyColumn: "SellerPageID",
                keyValue: 3,
                column: "Description",
                value: "Vi säljer dem bästa varorna i hela Göteborg!");

            migrationBuilder.UpdateData(
                table: "UserProducts",
                keyColumns: new[] { "ProductID", "UserID" },
                keyValues: new object[] { 1, 2 },
                column: "Amount",
                value: 10);

            migrationBuilder.UpdateData(
                table: "UserProducts",
                keyColumns: new[] { "ProductID", "UserID" },
                keyValues: new object[] { 5, 3 },
                column: "Amount",
                value: 12);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SellerPageID",
                table: "Products",
                column: "SellerPageID");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketplaceSellers_Marketplaces_MarketplaceID",
                table: "MarketplaceSellers",
                column: "MarketplaceID",
                principalTable: "Marketplaces",
                principalColumn: "MarketplaceID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MarketplaceSellers_Users_SellerID",
                table: "MarketplaceSellers",
                column: "SellerID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SellerPages_SellerPageID",
                table: "Products",
                column: "SellerPageID",
                principalTable: "SellerPages",
                principalColumn: "SellerPageID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SellerPageProducts_Products_ProductID",
                table: "SellerPageProducts",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_SellerPageProducts_SellerPages_SellerPageID",
                table: "SellerPageProducts",
                column: "SellerPageID",
                principalTable: "SellerPages",
                principalColumn: "SellerPageID");

            migrationBuilder.AddForeignKey(
                name: "FK_SellerPages_Users_SellerUserID",
                table: "SellerPages",
                column: "SellerUserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProducts_Products_ProductID",
                table: "UserProducts",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProducts_Users_UserID",
                table: "UserProducts",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");
        }
    }
}
