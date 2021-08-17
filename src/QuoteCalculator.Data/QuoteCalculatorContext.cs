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

            OnModelCreatingLoan(modelBuilder);
        }

        private void OnModelCreatingLoan(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Loan>()
                .Property(entity => entity.FirstName)
                .HasMaxLength(100).IsRequired();

            modelBuilder.Entity<Loan>()
                .Property(entity => entity.LastName)
                .HasMaxLength(100).IsRequired();

            modelBuilder.Entity<Loan>()
                .Property(entity => entity.Email)
                .HasMaxLength(100).IsRequired();

            modelBuilder.Entity<Loan>()
                .Property(entity => entity.MobileNumber)
                .HasMaxLength(20).IsRequired();

            modelBuilder.Entity<Loan>()
                .HasIndex(entity => new { entity.FirstName, entity.LastName })
                .IsUnique();
        }
    }
}
