using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce_pro.Migrations
{
    /// <inheritdoc />
    public partial class UserSingup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Otps");

            migrationBuilder.AddColumn<bool>(
                name: "IsBlock",
                table: "Usersingup",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBlock",
                table: "Usersingup");

            migrationBuilder.CreateTable(
                name: "Otps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OtpCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserSingupId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Otps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Otps_Usersingup_UserSingupId",
                        column: x => x.UserSingupId,
                        principalTable: "Usersingup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Otps_UserSingupId",
                table: "Otps",
                column: "UserSingupId");
        }
    }
}
