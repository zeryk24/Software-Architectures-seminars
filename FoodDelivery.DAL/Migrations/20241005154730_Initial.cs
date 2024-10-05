using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodDelivery.DAL.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Street = table.Column<string>(type: "TEXT", nullable: false),
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    PostalCode = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Disabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false),
                    AddressId = table.Column<int>(type: "INTEGER", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AddressEntity_AddressId",
                        column: x => x.AddressId,
                        principalTable: "AddressEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    MealType = table.Column<string>(type: "TEXT", nullable: false),
                    RestaurantId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealEntity_RestaurantEntity_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "RestaurantEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PaymentType = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true),
                    RestaurantId = table.Column<int>(type: "INTEGER", nullable: true),
                    AddressId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderEntity_AddressEntity_AddressId",
                        column: x => x.AddressId,
                        principalTable: "AddressEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderEntity_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderEntity_RestaurantEntity_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "RestaurantEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FeedbackEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    MealId = table.Column<int>(type: "INTEGER", nullable: true),
                    RestaurantId = table.Column<int>(type: "INTEGER", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedbackEntity_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FeedbackEntity_MealEntity_MealId",
                        column: x => x.MealId,
                        principalTable: "MealEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FeedbackEntity_RestaurantEntity_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "RestaurantEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItemEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UnitPrice = table.Column<double>(type: "REAL", nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: true),
                    MealId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItemEntity_MealEntity_MealId",
                        column: x => x.MealId,
                        principalTable: "MealEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderItemEntity_OrderEntity_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AddressEntity",
                columns: new[] { "Id", "City", "Number", "PostalCode", "Street" },
                values: new object[] { 1, "New York", "3818", "10016", "My Drive" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, "Admin", "ADMIN" },
                    { 2, null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddressId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, null, "27d178f3-32c5-4999-a64e-b2ad7f595b96", "admin@admin.com", true, false, null, "Admin", "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEG+Q65rGzfI9panrgB2C66ublj+MueDLj00lNlfjp62eb3lQgTMjSqL/PBPymj4jpw==", null, false, "D2F5C72C57E14290831FB92C609BB934", "Admin", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "RestaurantEntity",
                columns: new[] { "Id", "Disabled", "Name" },
                values: new object[,]
                {
                    { 1, false, "The Private Port" },
                    { 2, false, "The Sailing Stranger" },
                    { 3, false, "The Juniper Angel" },
                    { 4, false, "The Mountain Courtyard" },
                    { 5, false, "The Mountain Lantern" },
                    { 6, false, "Indigo" },
                    { 7, false, "The Peacock" },
                    { 8, false, "Sapphire" },
                    { 9, false, "The Nightingale" },
                    { 10, false, "Mumbles" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddressId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { 2, 0, 1, "fc20c0e1-259b-47e0-afd9-ec25d4653170", "emil@example.com", true, false, null, "Emil", "EMIL@EXAMPLE.COM", "EMIL@EXAMPLE.COM", "AQAAAAIAAYagAAAAEOm9WXkzKpTa7hnVAuYLAUYbg2+TydMgLguaZHU481hGl+rX/yPmh6669jYzp0CSOg==", null, false, "5B64691B7B2F458883046949F6F9695D", "Zelený", false, "emil@example.com" });

            migrationBuilder.InsertData(
                table: "MealEntity",
                columns: new[] { "Id", "Description", "MealType", "Name", "Price", "RestaurantId" },
                values: new object[,]
                {
                    { 1, "Fresh egg pasta in a sauce made from fresh broccoli and smoked pancetta", "Pasta", "Broccoli and pancetta fusilli", 10.5, 1 },
                    { 2, "A warm bagel filled with steak and parmesan", "Steak", "Steak and parmesan bagel", 15.199999999999999, 2 },
                    { 3, "Fresh egg tubular pasta in a sauce made from tuna and tangy lemon", "Pasta", "Tuna and lemon penne", 8.5999999999999996, 3 },
                    { 4, "Succulent burger made from chunky sausage and spicy chilli, served in a roll", "Burger", "Sausage and chilli burger", 16.5, 4 },
                    { 5, "A warm bagel filled with grouse and romaine lettuce", "Bagel", "Grouse and lettuce bagel", 5.0999999999999996, 5 },
                    { 6, "Sizzling sausages made from smoked tofu and pattypan squash, served in a roll", "Vegan", "Tofu and squash sausages", 7.0999999999999996, 6 },
                    { 7, "Fresh strawberries and sweet pepper combined into smooth soup", "Soup", "Strawberry and pepper soup", 8.0, 7 },
                    { 8, "Sole and fresh durian served on a bed of lettuce", "Salad", "Sole and durian salad", 10.5, 8 },
                    { 9, "A mouth-watering pasta salad served with garlic dressing", "Pasta salad", "Pasta salad with garlic dressing", 11.300000000000001, 9 },
                    { 10, "Fresh egg pasta in a sauce made from green pesto and baby spinach", "Pasta", "Pesto and spinach pasta", 8.0, 10 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 2, 2 });

            migrationBuilder.InsertData(
                table: "FeedbackEntity",
                columns: new[] { "Id", "Description", "MealId", "Rating", "RestaurantId", "UserId" },
                values: new object[,]
                {
                    { 1, "Too bad... Didn't like it at all.", 1, 1, null, 2 },
                    { 2, "Eh...", 2, 2, null, 2 },
                    { 3, "Not great, not terrible.", 3, 3, null, 2 },
                    { 4, "Liked it a lot!!", 4, 4, null, 2 },
                    { 5, "Awesome! Best thing I ever eaten!", 5, 5, null, 2 },
                    { 6, "Awesome! Best thing I ever eaten!", 6, 5, null, 2 },
                    { 7, "Liked it a lot!!", 7, 4, null, 2 },
                    { 8, "Not great, not terrible.", 8, 3, null, 2 },
                    { 9, "Eh...", 9, 2, null, 2 },
                    { 10, "Too bad... Didn't like it at all.", 10, 1, null, 2 },
                    { 11, "Really bad place.", null, 1, 1, 2 },
                    { 12, "Won't come again...", null, 2, 2, 2 },
                    { 13, "Could have been better.", null, 3, 3, 2 },
                    { 14, "Great place!", null, 4, 4, 2 },
                    { 15, "My favorite place! I highly recommend!", null, 5, 5, 2 },
                    { 16, "My favorite place! I highly recommend!", null, 5, 6, 2 },
                    { 17, "Great place!", null, 4, 7, 2 },
                    { 18, "Could have been better.", null, 3, 8, 2 },
                    { 19, "Won't come again...", null, 2, 9, 2 },
                    { 20, "Really bad place.", null, 1, 10, 2 }
                });

            migrationBuilder.InsertData(
                table: "OrderEntity",
                columns: new[] { "Id", "AddressId", "PaymentType", "RestaurantId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 0, 1, 2 },
                    { 2, 1, 0, 2, 2 },
                    { 3, 1, 0, 3, 2 },
                    { 4, 1, 0, 4, 2 },
                    { 5, 1, 0, 5, 2 },
                    { 6, 1, 1, 6, 2 },
                    { 7, 1, 1, 7, 2 },
                    { 8, 1, 1, 8, 2 },
                    { 9, 1, 1, 9, 2 },
                    { 10, 1, 2, 10, 2 }
                });

            migrationBuilder.InsertData(
                table: "OrderItemEntity",
                columns: new[] { "Id", "Amount", "MealId", "OrderId", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 2, 1, 1, 10.5 },
                    { 2, 3, 3, 1, 8.5999999999999996 },
                    { 3, 1, 2, 2, 15.199999999999999 },
                    { 4, 1, 3, 2, 8.5999999999999996 },
                    { 5, 2, 4, 2, 16.5 },
                    { 6, 1, 1, 3, 10.5 },
                    { 7, 1, 10, 3, 8.0 },
                    { 8, 5, 7, 4, 8.0 },
                    { 9, 2, 5, 5, 5.0999999999999996 },
                    { 10, 4, 9, 6, 11.300000000000001 },
                    { 11, 1, 7, 7, 8.0 },
                    { 12, 4, 8, 8, 10.5 },
                    { 13, 3, 9, 9, 11.300000000000001 },
                    { 14, 1, 6, 9, 7.0999999999999996 },
                    { 15, 1, 1, 10, 10.5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AddressId",
                table: "AspNetUsers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackEntity_MealId",
                table: "FeedbackEntity",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackEntity_RestaurantId",
                table: "FeedbackEntity",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackEntity_UserId",
                table: "FeedbackEntity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MealEntity_RestaurantId",
                table: "MealEntity",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEntity_AddressId",
                table: "OrderEntity",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEntity_RestaurantId",
                table: "OrderEntity",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEntity_UserId",
                table: "OrderEntity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemEntity_MealId",
                table: "OrderItemEntity",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemEntity_OrderId",
                table: "OrderItemEntity",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "FeedbackEntity");

            migrationBuilder.DropTable(
                name: "OrderItemEntity");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "MealEntity");

            migrationBuilder.DropTable(
                name: "OrderEntity");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "RestaurantEntity");

            migrationBuilder.DropTable(
                name: "AddressEntity");
        }
    }
}
