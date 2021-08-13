using QuoteCalculator.App.Manager;
using QuoteCalculator.App.Quotes.Models;

namespace QuoteCalculator.App.Quotes.Queries
{
    public class QuoteQuery : IQuoteQuery
    {
        public QuoteQuery() { }

        public void Execute(QuoteDetailModel model)
        {
            var loanManager = new LoanManager((double)model.FinanceAmount, (double)model.InterestRate, (int)model.Terms);
            model.RepaymentAmount = loanManager.RepaymentAmount;
            model.TotalInterest = loanManager.TotalInterest;
            model.QuoteSchedules = loanManager.GetSchedules();
        }
    }
}
