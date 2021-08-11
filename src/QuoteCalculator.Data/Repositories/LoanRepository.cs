using QuoteCalculator.Domain;

namespace QuoteCalculator.Data.Repositories
{
    public class LoanRepository : GenericRepository<Loan>
    {
        public LoanRepository(QuoteCalculatorContext context) : base(context) { }
    }
}
