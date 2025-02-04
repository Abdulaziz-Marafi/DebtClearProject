using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DebtClearProject.Migrations
{
    /// <inheritdoc />
    public partial class TransAddedGuidToDebtFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Debts_DebtId1",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_DebtId1",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DebtId1",
                table: "Transactions");

            migrationBuilder.AlterColumn<Guid>(
                name: "DebtId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DebtId",
                table: "Transactions",
                column: "DebtId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Debts_DebtId",
                table: "Transactions",
                column: "DebtId",
                principalTable: "Debts",
                principalColumn: "DebtId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Debts_DebtId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_DebtId",
                table: "Transactions");

            migrationBuilder.AlterColumn<string>(
                name: "DebtId",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "DebtId1",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DebtId1",
                table: "Transactions",
                column: "DebtId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Debts_DebtId1",
                table: "Transactions",
                column: "DebtId1",
                principalTable: "Debts",
                principalColumn: "DebtId");
        }
    }
}
