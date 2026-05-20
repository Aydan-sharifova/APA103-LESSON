using System;
using _27_FrontToBackSqlConnection.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace _27_FrontToBackSqlConnection.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20260520114812_AddTagsAndProductTags")]
public partial class AddTagsAndProductTags : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Tags",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                isDeleted = table.Column<bool>(type: "boolean", nullable: false),
                Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Tags", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "ProductTags",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                ProductId = table.Column<int>(type: "integer", nullable: false),
                TagId = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductTags", x => x.Id);
                table.ForeignKey(
                    name: "FK_ProductTags_Products_ProductId",
                    column: x => x.ProductId,
                    principalTable: "Products",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_ProductTags_Tags_TagId",
                    column: x => x.TagId,
                    principalTable: "Tags",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_ProductTags_ProductId",
            table: "ProductTags",
            column: "ProductId");

        migrationBuilder.CreateIndex(
            name: "IX_ProductTags_TagId",
            table: "ProductTags",
            column: "TagId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ProductTags");

        migrationBuilder.DropTable(
            name: "Tags");
    }
}
