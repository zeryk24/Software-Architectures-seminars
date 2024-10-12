using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    GoodsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name_Value = table.Column<string>(type: "TEXT", nullable: false),
                    Amount_UnitsAmount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.GoodsId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Address_State = table.Column<int>(type: "INTEGER", nullable: false),
                    Address_City = table.Column<string>(type: "TEXT", nullable: false),
                    Address_Code = table.Column<string>(type: "TEXT", nullable: false),
                    Address_StreetAndNumber = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Goods_Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Amount_Value = table.Column<int>(type: "INTEGER", nullable: false),
                    Price_Value = table.Column<int>(type: "INTEGER", nullable: false),
                    Price_Currency = table.Column<int>(type: "INTEGER", nullable: false),
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
                name: "Goods");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
