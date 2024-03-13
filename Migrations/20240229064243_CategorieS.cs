using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce_pro.Migrations
{
    /// <inheritdoc />
    public partial class CategorieS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsList",
                table: "categories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsList",
                table: "categories");
        }
    }
}
