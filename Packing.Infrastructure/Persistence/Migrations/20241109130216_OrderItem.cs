using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Packing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class OrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Product_GoodsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Product_Amount = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderItemId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderItemId",
                table: "OrderItems",
                column: "OrderItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");
        }
    }
}
