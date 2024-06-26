using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ListOfItems.Migrations
{
    /// <inheritdoc />
    public partial class CreateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category_Master",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Avaliable = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category_Master", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "SubCategory_Master",
                columns: table => new
                {
                    SubCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubCategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Avaliable = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategory_Master", x => x.SubCategoryId);
                    table.ForeignKey(
                        name: "FK_SubCategory_Master_Category_Master_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category_Master",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemMaster",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemMaster", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_ItemMaster_Category_Master_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category_Master",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemMaster_SubCategory_Master_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategory_Master",
                        principalColumn: "SubCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemMaster_CategoryId",
                table: "ItemMaster",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMaster_SubCategoryId",
                table: "ItemMaster",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_Master_CategoryId",
                table: "SubCategory_Master",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemMaster");

            migrationBuilder.DropTable(
                name: "SubCategory_Master");

            migrationBuilder.DropTable(
                name: "Category_Master");
        }
    }
}
