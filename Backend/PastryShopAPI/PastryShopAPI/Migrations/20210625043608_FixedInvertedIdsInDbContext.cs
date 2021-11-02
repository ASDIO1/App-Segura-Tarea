using Microsoft.EntityFrameworkCore.Migrations;

namespace PastryShopAPI.Migrations
{
    public partial class FixedInvertedIdsInDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Combos_Products_ComboId",
                table: "Product_Combos");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Combos_Combos_ProductId",
                table: "Product_Combos");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Combos_Combos_ComboId",
                table: "Product_Combos",
                column: "ComboId",
                principalTable: "Combos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Combos_Products_ProductId",
                table: "Product_Combos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Combos_Combos_ComboId",
                table: "Product_Combos");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Combos_Products_ProductId",
                table: "Product_Combos");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Combos_Products_ComboId",
                table: "Product_Combos",
                column: "ComboId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Combos_Combos_ProductId",
                table: "Product_Combos",
                column: "ProductId",
                principalTable: "Combos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
