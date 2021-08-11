using QuoteCalculator.App.Loans.Models;

namespace QuoteCalculator.App.Loans.Queries
{
    public interface ILoanQuery
    {
        void Execute(LoanListModel model);
        void Execute(LoanDetailModel model);
        void Execute(EditLoanModel model);
    }
}
