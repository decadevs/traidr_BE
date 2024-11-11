using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace traidr.Domain.Migrations
{
    /// <inheritdoc />
    public partial class CreateShopTableAndModifyUserAndProductCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategories_ProductCategory",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductCategory",
                table: "Products",
                newName: "ProductCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductCategory",
                table: "Products",
                newName: "IX_Products_ProductCategoryId");

            migrationBuilder.RenameColumn(
                name: "ShopName",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "ProductElements",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string[]>(
                name: "SubCategories",
                table: "ProductCategories",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReferralSource",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShopName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    SellerId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shops_AspNetUsers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shops_SellerId",
                table: "Shops",
                column: "SellerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategories_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategories_ProductCategoryId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "ProductElements");

            migrationBuilder.DropColumn(
                name: "SubCategories",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ReferralSource",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ProductCategoryId",
                table: "Products",
                newName: "ProductCategory");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductCategoryId",
                table: "Products",
                newName: "IX_Products_ProductCategory");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AspNetUsers",
                newName: "ShopName");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategories_ProductCategory",
                table: "Products",
                column: "ProductCategory",
                principalTable: "ProductCategories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
