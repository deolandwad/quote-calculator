using QuoteCalculator.App.Quotes.Models;
using QuoteCalculator.App.Quotes.Queries;
using System;
using System.Collections.Generic;
using Xunit;

namespace QuoteCalculator.Tests.App.Quotes
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

            Assert.Equal(RepaymentAmount, model.RepaymentAmount);
        }

        [Fact]
        public void TestExecuteShouldCalculateTotalInterest()
        {
            sut.Execute(model);

            Assert.Equal(TotalInterest, model.TotalInterest);
        }

        [Fact]
        public void TestExecuteShouldReturnQuoteSchedules()
        {
            var expectedCount = 2;
            var expected = new List<QuoteScheduleModel>()
            {
                new QuoteScheduleModel()
                {
                    PaymentNo = 1,
                    Payment = 2514.38,
                    Principal = 2495.22,
                    Interest = 19.17,
                    Balance = 2504.78
                },
                new QuoteScheduleModel()
                {
                    PaymentNo = 2,
                    Payment = 2514.38,
                    Principal = 2504.78,
                    Interest = 9.6,
                    Balance = 0
                }
            };

            model.Terms = 2;
            model.InterestRate = 4.6;
            model.FinanceAmount = 5000;

            sut.Execute(model);

            var results = model.QuoteSchedules;

            Assert.Equal(expectedCount, results.Count);

            int index = 0;
            foreach (var result in results)
            {
                Assert.Equal(expected[index].PaymentNo, result.PaymentNo);
                Assert.Equal(expected[index].Payment, Math.Round(result.Payment, 2));
                Assert.Equal(expected[index].Principal, Math.Round(result.Principal, 2));
                Assert.Equal(expected[index].Interest, Math.Round(result.Interest, 2));
                Assert.Equal(expected[index].Balance, Math.Round(result.Balance, 2));
                index++;
            }
        }

        [Fact]
        public void TestExecuteShouldThrowTermsException()
        {
            model.Terms = 0;
            string termsException = $"terms cannot be {model.Terms}";

            var ex = Assert.Throws<ArgumentException>(() => sut.Execute(model));

            Assert.Equal(termsException, ex.Message);
        }

        [Fact]
        public void TestExecuteShouldThrowInterestRateException()
        {
            model.InterestRate = -1;
            string interestException = $"interest cannot be {model.InterestRate}";

            var ex = Assert.Throws<ArgumentException>(() => sut.Execute(model));

            Assert.Equal(interestException, ex.Message);
        }

        [Fact]
        public void TestExecuteShouldThrowFinanceAmountException()
        {
            model.FinanceAmount = -1;
            string amountException = $"amount cannot be {model.FinanceAmount}";

            var ex = Assert.Throws<ArgumentException>(() => sut.Execute(model));

            Assert.Equal(amountException, ex.Message);
        }
    }
}
