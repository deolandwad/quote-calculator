using Microsoft.EntityFrameworkCore.Migrations;

namespace QuoteCalculator.Data.Migrations
{
    public partial class UpdateLoanPhoneToMobileNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Loans",
                newName: "MobileNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MobileNumber",
                table: "Loans",
                newName: "Phone");
        }
    }
}
