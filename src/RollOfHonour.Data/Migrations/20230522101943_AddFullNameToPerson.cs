using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RollOfHonour.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFullNameToPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "People",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.Sql("UPDATE dbo.People SET FullName = CONCAT(FirstNames, ' ', LastName)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "People");
        }
    }
}
