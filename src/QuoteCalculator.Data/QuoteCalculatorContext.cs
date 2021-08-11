using Microsoft.EntityFrameworkCore;
using QuoteCalculator.Domain;

namespace QuoteCalculator.Data
{
    public class QuoteCalculatorContext : DbContext
    {
        public QuoteCalculatorContext() { }

        public QuoteCalculatorContext(DbContextOptions<QuoteCalculatorContext> options) : base(options) { }

        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
