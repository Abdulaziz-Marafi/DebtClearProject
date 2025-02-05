using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DebtClearProject.Migrations
{
    /// <inheritdoc />
    public partial class fixedUserDebts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "UserDebts");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "UserDebts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserDebts");

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "UserDebts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
