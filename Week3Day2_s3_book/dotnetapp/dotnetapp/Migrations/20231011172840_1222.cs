using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetapp.Migrations
{
    public partial class _1222 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ShoppingCartItem",
                columns: new[] { "Id", "BookId", "Quantity", "ShoppingCartId" },
                values: new object[] { 1, 1, 1, null });

            migrationBuilder.InsertData(
                table: "ShoppingCartItem",
                columns: new[] { "Id", "BookId", "Quantity", "ShoppingCartId" },
                values: new object[] { 2, 2, 2, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ShoppingCartItem",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ShoppingCartItem",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
