using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce_pro.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneNumberToUsersingup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Usersingup",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Usersingup");
        }
    }
}
