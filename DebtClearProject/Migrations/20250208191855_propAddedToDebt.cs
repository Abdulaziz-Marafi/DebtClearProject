using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DebtClearProject.Migrations
{
    /// <inheritdoc />
    public partial class propAddedToDebt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DebtName",
                table: "Debts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DebtName",
                table: "Debts");
        }
    }
}
