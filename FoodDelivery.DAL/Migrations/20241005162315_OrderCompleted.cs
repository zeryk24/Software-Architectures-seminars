using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDelivery.DAL.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class OrderCompleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "OrderEntity",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f60c99fd-648b-4385-942c-f6fa5ee24588", "AQAAAAIAAYagAAAAENouXHvTRXg5qgfR7CkJmVW60VA6/lKTrHH5YyPJKGAw4cmuUf91+mg+dlwkzKntFA==", "148F1E57B2114E4EABF6BA18DE1D778E" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "66401ef0-d649-4cb7-91cc-3115866815b5", "AQAAAAIAAYagAAAAEKNorJGwC8Ih6jXXzoJBenzyZEHqZcJK6qzsGtWZXDdY2RqPDA+NNx2cyW4ULDFi5g==", "B6A9637029384D8B902DBA53725AAF12" });

            migrationBuilder.UpdateData(
                table: "OrderEntity",
                keyColumn: "Id",
                keyValue: 1,
                column: "Completed",
                value: false);

            migrationBuilder.UpdateData(
                table: "OrderEntity",
                keyColumn: "Id",
                keyValue: 2,
                column: "Completed",
                value: false);

            migrationBuilder.UpdateData(
                table: "OrderEntity",
                keyColumn: "Id",
                keyValue: 3,
                column: "Completed",
                value: false);

            migrationBuilder.UpdateData(
                table: "OrderEntity",
                keyColumn: "Id",
                keyValue: 4,
                column: "Completed",
                value: false);

            migrationBuilder.UpdateData(
                table: "OrderEntity",
                keyColumn: "Id",
                keyValue: 5,
                column: "Completed",
                value: false);

            migrationBuilder.UpdateData(
                table: "OrderEntity",
                keyColumn: "Id",
                keyValue: 6,
                column: "Completed",
                value: false);

            migrationBuilder.UpdateData(
                table: "OrderEntity",
                keyColumn: "Id",
                keyValue: 7,
                column: "Completed",
                value: false);

            migrationBuilder.UpdateData(
                table: "OrderEntity",
                keyColumn: "Id",
                keyValue: 8,
                column: "Completed",
                value: false);

            migrationBuilder.UpdateData(
                table: "OrderEntity",
                keyColumn: "Id",
                keyValue: 9,
                column: "Completed",
                value: false);

            migrationBuilder.UpdateData(
                table: "OrderEntity",
                keyColumn: "Id",
                keyValue: 10,
                column: "Completed",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "OrderEntity");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "27d178f3-32c5-4999-a64e-b2ad7f595b96", "AQAAAAIAAYagAAAAEG+Q65rGzfI9panrgB2C66ublj+MueDLj00lNlfjp62eb3lQgTMjSqL/PBPymj4jpw==", "D2F5C72C57E14290831FB92C609BB934" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fc20c0e1-259b-47e0-afd9-ec25d4653170", "AQAAAAIAAYagAAAAEOm9WXkzKpTa7hnVAuYLAUYbg2+TydMgLguaZHU481hGl+rX/yPmh6669jYzp0CSOg==", "5B64691B7B2F458883046949F6F9695D" });
        }
    }
}
