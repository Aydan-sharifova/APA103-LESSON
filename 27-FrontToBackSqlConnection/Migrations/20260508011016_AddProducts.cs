using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _27_FrontToBackSqlConnection.Migrations
{
    /// <inheritdoc />
    public partial class AddProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoverImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    IsNew = table.Column<bool>(type: "bit", nullable: false),
                    IsBestSeller = table.Column<bool>(type: "bit", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "Description", "HoverImage", "Image", "IsBestSeller", "IsFeatured", "IsNew", "Name", "Price", "isDeleted" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "A bright flowering plant for balconies, gardens, and warm indoor corners.", "1-2-270x300.jpg", "1-1-270x300.jpg", true, true, false, "American Marigold", 23.45m, false },
                    { 2, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Compact, cheerful blooms with easy care needs and long seasonal color.", "1-3-270x300.jpg", "1-2-270x300.jpg", false, true, false, "Black Eyed Susan", 25.45m, false },
                    { 3, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Elegant heart-shaped flowers for soft shade and decorative garden beds.", "1-4-270x300.jpg", "1-3-270x300.jpg", false, false, true, "Bleeding Heart", 30.45m, false },
                    { 4, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "A resilient perennial with rich color and a tidy spreading habit.", "1-5-270x300.jpg", "1-4-270x300.jpg", true, false, false, "Bloody Cranesbill", 45.00m, false },
                    { 5, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "A sun-loving plant with vivid blooms and pollinator-friendly growth.", "1-6-270x300.jpg", "1-5-270x300.jpg", false, false, true, "Butterfly Weed", 50.45m, false },
                    { 6, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hardy clusters of flowers that fit naturally into low-maintenance gardens.", "1-7-270x300.jpg", "1-6-270x300.jpg", false, true, false, "Common Yarrow", 65.00m, false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
