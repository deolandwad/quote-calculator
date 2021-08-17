using QuoteCalculator.App.Manager;
using QuoteCalculator.App.Quotes.Models;
using QuoteCalculator.Data;
using System.Linq;

namespace QuoteCalculator.App.Quotes.Queries
{
    public class QuoteQuery : IQuoteQuery
    {
        private readonly IUnitOfWork unitOfWork;

        public QuoteQuery(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Execute(QuoteDetailModel model)
        {
            var loans = unitOfWork.LoanRepository.Find(loan => loan.FirstName == model.FirstName && loan.LastName == model.LastName);
            model.isDuplicateLoan = (loans.Count() > 0) ? true : false;

            var loanManager = new LoanManager((double)model.FinanceAmount, (double)model.InterestRate, (int)model.Terms);
            model.RepaymentAmount = loanManager.RepaymentAmount;
            model.TotalInterest = loanManager.TotalInterest;
            model.QuoteSchedules = loanManager.GetSchedules();
        }
    }
}
