using QuoteCalculator.App.Quotes.Models;
using QuoteCalculator.App.Quotes.Queries;
using Xunit;

namespace QuoteCalculator.Tests.App
{
    public class QuoteQueryTests
    {
        private IQuoteQuery sut;
        private QuoteDetailModel model;

        private const int Terms = 120;
        private const double InterestRate = 5;
        private const double FinanceAmount = 20000;
        private const double RepaymentAmount = 212.13;
        private const double TotalInterest = 5455.72;

        public QuoteQueryTests()
        {
            model = new QuoteDetailModel()
            {
                Terms = Terms,
                InterestRate = InterestRate,
                FinanceAmount = FinanceAmount
            };

            sut = new QuoteQuery();
        }

        [Fact]
        public void TestExecuteShouldCalculateRepaymentAmount()
        {
            sut.Execute(model);

            Assert.Equal(model.RepaymentAmount, RepaymentAmount);
        }

        [Fact]
        public void TestExecuteShouldCalculateTotalInterest()
        {
            sut.Execute(model);

            Assert.Equal(model.TotalInterest, TotalInterest);
        }
    }
}
