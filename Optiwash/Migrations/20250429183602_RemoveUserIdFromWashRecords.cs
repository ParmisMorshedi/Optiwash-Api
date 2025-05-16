using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptiWash.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserIdFromWashRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WashRecords_AspNetUsers_UserId",
                table: "WashRecords");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "WashRecords",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_WashRecords_AspNetUsers_UserId",
                table: "WashRecords",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WashRecords_AspNetUsers_UserId",
                table: "WashRecords");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "WashRecords",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WashRecords_AspNetUsers_UserId",
                table: "WashRecords",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
