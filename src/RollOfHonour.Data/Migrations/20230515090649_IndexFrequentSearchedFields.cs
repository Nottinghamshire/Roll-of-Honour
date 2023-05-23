using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RollOfHonour.Data.Migrations
{
    /// <inheritdoc />
    public partial class IndexFrequentSearchedFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "WarMemorials",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SubUnits",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Rank",
                table: "People",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "People",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstNames",
                table: "People",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarMemorials_Name",
                table: "WarMemorials",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_SubUnits_Name",
                table: "SubUnits",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_People_DateOfBirth",
                table: "People",
                column: "DateOfBirth");

            migrationBuilder.CreateIndex(
                name: "IX_People_DateOfDeath",
                table: "People",
                column: "DateOfDeath");

            migrationBuilder.CreateIndex(
                name: "IX_People_FirstNames",
                table: "People",
                column: "FirstNames");

            migrationBuilder.CreateIndex(
                name: "IX_People_LastName",
                table: "People",
                column: "LastName");

            migrationBuilder.CreateIndex(
                name: "IX_People_Rank",
                table: "People",
                column: "Rank");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WarMemorials_Name",
                table: "WarMemorials");

            migrationBuilder.DropIndex(
                name: "IX_SubUnits_Name",
                table: "SubUnits");

            migrationBuilder.DropIndex(
                name: "IX_People_DateOfBirth",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_DateOfDeath",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_FirstNames",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_LastName",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_Rank",
                table: "People");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "WarMemorials",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SubUnits",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Rank",
                table: "People",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "People",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstNames",
                table: "People",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
