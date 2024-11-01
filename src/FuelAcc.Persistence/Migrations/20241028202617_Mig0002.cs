using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuelAcc.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Mig0002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rests_Products_StorageId",
                table: "Rests");

            migrationBuilder.CreateIndex(
                name: "IX_Rests_ProductId",
                table: "Rests",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rests_Products_ProductId",
                table: "Rests",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rests_Products_ProductId",
                table: "Rests");

            migrationBuilder.DropIndex(
                name: "IX_Rests_ProductId",
                table: "Rests");

            migrationBuilder.AddForeignKey(
                name: "FK_Rests_Products_StorageId",
                table: "Rests",
                column: "StorageId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
