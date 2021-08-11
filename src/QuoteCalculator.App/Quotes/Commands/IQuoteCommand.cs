using QuoteCalculator.App.Quotes.Models;

namespace QuoteCalculator.App.Quotes.Commands
{
    public interface IQuoteCommand
    {
        void Execute(QuoteDetailModel model);
    }
}
