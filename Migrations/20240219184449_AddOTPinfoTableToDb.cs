using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce_pro.Migrations
{
    /// <inheritdoc />
    public partial class AddOTPinfoTableToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Otps_Usersingup_UserId",
                table: "Otps");

            migrationBuilder.DropIndex(
                name: "IX_Otps_UserId",
                table: "Otps");

            migrationBuilder.AddColumn<int>(
                name: "UserSingupId",
                table: "Otps",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OTPinfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OTp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    expertime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTPinfo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Otps_UserSingupId",
                table: "Otps",
                column: "UserSingupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Otps_Usersingup_UserSingupId",
                table: "Otps",
                column: "UserSingupId",
                principalTable: "Usersingup",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Otps_Usersingup_UserSingupId",
                table: "Otps");

            migrationBuilder.DropTable(
                name: "OTPinfo");

            migrationBuilder.DropIndex(
                name: "IX_Otps_UserSingupId",
                table: "Otps");

            migrationBuilder.DropColumn(
                name: "UserSingupId",
                table: "Otps");

            migrationBuilder.CreateIndex(
                name: "IX_Otps_UserId",
                table: "Otps",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Otps_Usersingup_UserId",
                table: "Otps",
                column: "UserId",
                principalTable: "Usersingup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
