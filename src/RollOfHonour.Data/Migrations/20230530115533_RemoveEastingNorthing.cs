using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RollOfHonour.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEastingNorthing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Easting",
                table: "WarMemorials");

            migrationBuilder.DropColumn(
                name: "Northing",
                table: "WarMemorials");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Easting",
                table: "WarMemorials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Northing",
                table: "WarMemorials",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
