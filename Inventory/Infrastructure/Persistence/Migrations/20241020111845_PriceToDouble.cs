using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PriceToDouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderItemId",
                table: "OrderItems");

            migrationBuilder.AlterColumn<double>(
                name: "Price_Value",
                table: "OrderItems",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderItemId",
                table: "OrderItems",
                column: "OrderItemId",
                principalTable: "Orders",
                principalColumn: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderItemId",
                table: "OrderItems");

            migrationBuilder.AlterColumn<int>(
                name: "Price_Value",
                table: "OrderItems",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderItemId",
                table: "OrderItems",
                column: "OrderItemId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
