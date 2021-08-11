using Microsoft.EntityFrameworkCore.Migrations;

namespace QuoteCalculator.Data.Migrations
{
    public partial class AddRepaymentAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Interest",
                table: "Loans",
                newName: "RepaymentAmount");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Loans",
                newName: "InterestRate");

            migrationBuilder.AddColumn<double>(
                name: "FinanceAmount",
                table: "Loans",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinanceAmount",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "RepaymentAmount",
                table: "Loans",
                newName: "Interest");

            migrationBuilder.RenameColumn(
                name: "InterestRate",
                table: "Loans",
                newName: "Amount");
        }
    }
}
