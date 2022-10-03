using Microsoft.EntityFrameworkCore.Migrations;

namespace wize.commerce.data.Migrations
{
    public partial class UpdateProductInventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Products_ProductId",
                table: "Inventory");

            migrationBuilder.DropIndex(
                name: "IX_Products_InventoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_ProductId",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Inventory");

            migrationBuilder.CreateIndex(
                name: "IX_Products_InventoryId",
                table: "Products",
                column: "InventoryId",
                unique: true,
                filter: "[InventoryId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_InventoryId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Inventory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_InventoryId",
                table: "Products",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ProductId",
                table: "Inventory",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Products_ProductId",
                table: "Inventory",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
