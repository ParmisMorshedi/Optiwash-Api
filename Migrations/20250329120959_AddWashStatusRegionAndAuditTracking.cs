using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptiWash.Migrations
{
    /// <inheritdoc />
    public partial class AddWashStatusRegionAndAuditTracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "WashRecords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "WashRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "WashRecords",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PlateNumber",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Cars",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Cars",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_OrganizationId",
                table: "Cars",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Organization_OrganizationId",
                table: "Cars",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Organization_OrganizationId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropIndex(
                name: "IX_Cars_OrganizationId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "WashRecords");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "WashRecords");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "WashRecords");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Cars");

            migrationBuilder.AlterColumn<string>(
                name: "PlateNumber",
                table: "Cars",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
