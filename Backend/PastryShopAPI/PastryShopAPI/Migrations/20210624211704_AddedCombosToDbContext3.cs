using Microsoft.EntityFrameworkCore.Migrations;

namespace PastryShopAPI.Migrations
{
    public partial class AddedCombosToDbContext3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Combos_ComboEntity_ProductId",
                table: "Product_Combos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComboEntity",
                table: "ComboEntity");

            migrationBuilder.RenameTable(
                name: "ComboEntity",
                newName: "Combos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Combos",
                table: "Combos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Combos_Combos_ProductId",
                table: "Product_Combos",
                column: "ProductId",
                principalTable: "Combos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Combos_Combos_ProductId",
                table: "Product_Combos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Combos",
                table: "Combos");

            migrationBuilder.RenameTable(
                name: "Combos",
                newName: "ComboEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComboEntity",
                table: "ComboEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Combos_ComboEntity_ProductId",
                table: "Product_Combos",
                column: "ProductId",
                principalTable: "ComboEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
