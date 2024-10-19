using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedOrderProcessed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderItemId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "OrderAddress_StreetAndNumber",
                table: "Orders",
                newName: "Address_StreetAndNumber");

            migrationBuilder.RenameColumn(
                name: "OrderAddress_State",
                table: "Orders",
                newName: "Address_State");

            migrationBuilder.RenameColumn(
                name: "OrderAddress_Code",
                table: "Orders",
                newName: "Address_Code");

            migrationBuilder.RenameColumn(
                name: "OrderAddress_City",
                table: "Orders",
                newName: "Address_City");

            migrationBuilder.AddColumn<bool>(
                name: "Processed_IsProcessed",
                table: "Orders",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Processed_ProcessedAt",
                table: "Orders",
                type: "TEXT",
                nullable: true);

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

            migrationBuilder.DropColumn(
                name: "Processed_IsProcessed",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Processed_ProcessedAt",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Address_StreetAndNumber",
                table: "Orders",
                newName: "OrderAddress_StreetAndNumber");

            migrationBuilder.RenameColumn(
                name: "Address_State",
                table: "Orders",
                newName: "OrderAddress_State");

            migrationBuilder.RenameColumn(
                name: "Address_Code",
                table: "Orders",
                newName: "OrderAddress_Code");

            migrationBuilder.RenameColumn(
                name: "Address_City",
                table: "Orders",
                newName: "OrderAddress_City");

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
