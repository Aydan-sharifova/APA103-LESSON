using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _27_FrontToBackSqlConnection.Migrations
{
    /// <inheritdoc />
    public partial class AddProductRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoverImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    IsNew = table.Column<bool>(type: "bit", nullable: false),
                    IsBestSeller = table.Column<bool>(type: "bit", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name", "isDeleted" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "House Plants", false },
                    { 2, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Garden Plants", false },
                    { 3, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gift Plants", false }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "HoverImage", "Image", "IsBestSeller", "IsFeatured", "IsNew", "Name", "Price", "SKU", "isDeleted" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "A bright flowering plant for balconies, gardens, and warm indoor corners.", "1-2-270x300.jpg", "1-1-270x300.jpg", true, true, false, "American Marigold", 23.45m, "PL-001", false },
                    { 2, 2, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Compact, cheerful blooms with easy care needs and long seasonal color.", "1-3-270x300.jpg", "1-2-270x300.jpg", false, true, false, "Black Eyed Susan", 25.45m, "PL-002", false },
                    { 3, 3, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Elegant heart-shaped flowers for soft shade and decorative garden beds.", "1-4-270x300.jpg", "1-3-270x300.jpg", false, false, true, "Bleeding Heart", 30.45m, "PL-003", false },
                    { 4, 1, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "A resilient perennial with rich color and a tidy spreading habit.", "1-5-270x300.jpg", "1-4-270x300.jpg", true, false, false, "Bloody Cranesbill", 45.00m, "PL-004", false },
                    { 5, 1, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "A sun-loving plant with vivid blooms and pollinator-friendly growth.", "1-6-270x300.jpg", "1-5-270x300.jpg", false, false, true, "Butterfly Weed", 50.45m, "PL-005", false },
                    { 6, 3, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hardy clusters of flowers that fit naturally into low-maintenance gardens.", "1-7-270x300.jpg", "1-6-270x300.jpg", false, true, false, "Common Yarrow", 65.00m, "PL-006", false }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "CreatedAt", "Image", "IsPrimary", "ProductId", "isDeleted" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "1-1-270x300.jpg", true, 1, false },
                    { 2, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "1-2-270x300.jpg", false, 1, false },
                    { 3, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "1-2-270x300.jpg", true, 2, false },
                    { 4, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "1-3-270x300.jpg", false, 2, false },
                    { 5, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "1-3-270x300.jpg", true, 3, false },
                    { 6, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "1-4-270x300.jpg", true, 4, false },
                    { 7, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "1-5-270x300.jpg", true, 5, false },
                    { 8, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "1-6-270x300.jpg", true, 6, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
