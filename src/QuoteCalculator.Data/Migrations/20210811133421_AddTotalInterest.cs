using Microsoft.EntityFrameworkCore.Migrations;

namespace QuoteCalculator.Data.Migrations
{
    public partial class AddTotalInterest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalInterest",
                table: "Loans",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalInterest",
                table: "Loans");
        }
    }
}
