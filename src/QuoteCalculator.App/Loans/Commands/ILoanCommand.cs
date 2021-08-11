using QuoteCalculator.App.Loans.Models;

namespace QuoteCalculator.App.Loans.Commands
{
    public interface ILoanCommand
    {
        void Execute(EditLoanModel model);
        void Execute(DeleteLoanModel model);
    }
}
