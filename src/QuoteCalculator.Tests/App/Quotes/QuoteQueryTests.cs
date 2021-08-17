using Moq;
using QuoteCalculator.App.Quotes.Models;
using QuoteCalculator.App.Quotes.Queries;
using QuoteCalculator.Data;
using QuoteCalculator.Data.Repositories;
using QuoteCalculator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace QuoteCalculator.Tests.App.Quotes
{
    public class QuoteQueryTests
    {
        private Mock<IRepository<Loan>> mockLoanRepository;
        private Mock<IUnitOfWork> mockUnitOfWork;
        private IQuoteQuery sut;
        private QuoteDetailModel model;
        private List<Loan> loans;

        private const int Id = 0;
        private const string FirstName = "Cammy";
        private const string LastName = "Albares";
        private const string Email = "calbares@gmail.com";
        private const string MobileNumber = "956-537-6195";
        private const int Terms = 120;
        private const double InterestRate = 5;
        private const double FinanceAmount = 20000;
        private const double RepaymentAmount = 212.13;
        private const double TotalInterest = 5455.72;

        public QuoteQueryTests()
        {
            model = new QuoteDetailModel()
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                MobileNumber = MobileNumber,
                Terms = Terms,
                InterestRate = InterestRate,
                FinanceAmount = FinanceAmount
            };

            loans = new List<Loan>()
            {
                new Loan()
                {
                    Id = 1,
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    MobileNumber = MobileNumber,
                    Terms = Terms,
                    InterestRate = InterestRate,
                    FinanceAmount = FinanceAmount,
                    RepaymentAmount = RepaymentAmount,
                    TotalInterest = TotalInterest
                }
            };

            mockLoanRepository = new Mock<IRepository<Loan>>();
            mockLoanRepository.Setup(m => m.Find(It.IsAny<Expression<Func<Loan, bool>>>()))
                .Returns(new Func<Expression<Func<Loan, bool>>, IQueryable<Loan>>(expr => loans.Where(expr.Compile()).AsQueryable()));

            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.LoanRepository).Returns(mockLoanRepository.Object);

            sut = new QuoteQuery(mockUnitOfWork.Object);
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

        [Fact]
        public void TestExecuteHasExistingLoan()
        {
            sut.Execute(model);

            mockLoanRepository.Verify(m => m.Find(It.IsAny<Expression<Func<Loan, bool>>>()), Times.Once());
            Assert.True(model.isDuplicateLoan);
        }

        [Fact]
        public void TestExecuteHasNoExistingLoan()
        {
            model.FirstName = "Roxane";
            model.LastName = "Campain";

            sut.Execute(model);

            mockLoanRepository.Verify(m => m.Find(It.IsAny<Expression<Func<Loan, bool>>>()), Times.Once());
            Assert.False(model.isDuplicateLoan);
        }
    }
}
