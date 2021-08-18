using QuoteCalculator.Data.Repositories;
using QuoteCalculator.Domain;

namespace QuoteCalculator.Data
{
    public class QuoteCalculatorUnitOfWork : IUnitOfWork
    {
        private QuoteCalculatorContext context;

        public QuoteCalculatorUnitOfWork(QuoteCalculatorContext context)
        {
            this.context = context;
        }

        private IRepository<Loan> loanRepository;
        public IRepository<Loan> LoanRepository
        {
            get
            {
                if (loanRepository == null)
                {
                    loanRepository = new LoanRepository(context);
                }

                return loanRepository;
            }
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
