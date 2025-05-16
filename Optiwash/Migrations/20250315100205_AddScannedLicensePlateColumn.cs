using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptiWash.Migrations
{
    /// <inheritdoc />
    public partial class AddScannedLicensePlateColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ScannedLicensePlate",
                table: "Cars",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScannedLicensePlate",
                table: "Cars");
        }
    }
}
