using QuoteCalculator.App.Quotes.Models;

namespace QuoteCalculator.App.Quotes.Queries
{
    public interface IQuoteQuery
    {
        void Execute(QuoteDetailModel model);
    }
}
