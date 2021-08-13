using AutoMapper;
using Moq;
using QuoteCalculator.App.Mapping;
using QuoteCalculator.App.Quotes.Commands;
using QuoteCalculator.App.Quotes.Models;
using QuoteCalculator.Data;
using QuoteCalculator.Data.Repositories;
using QuoteCalculator.Domain;
using Xunit;

namespace QuoteCalculator.Tests.App.Quotes
{
    public class QuoteCommandTests
    {
        private Mock<IRepository<Loan>> mockLoanRepository;
        private Mock<IUnitOfWork> mockUnitOfWork;
        private IMapper mapper;
        private IQuoteCommand sut;
        private QuoteDetailModel model;
        private Loan loan;

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

        public QuoteCommandTests()
        {
            if (mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper instanceMapper = mappingConfig.CreateMapper();
                mapper = instanceMapper;
            }

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

            loan = new Loan()
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
            };

            mockLoanRepository = new Mock<IRepository<Loan>>();
            mockLoanRepository.Setup(m => m.Get(loan.Id)).Returns(loan);

            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.LoanRepository).Returns(mockLoanRepository.Object);

            sut = new QuoteCommand(mockUnitOfWork.Object, mapper);
        }

        [Fact]
        public void TestExecuteShouldRecomputeRepaymentAmount()
        {
            sut.Execute(model);

            Assert.Equal(RepaymentAmount, model.RepaymentAmount);
        }

        [Fact]
        public void TestExecuteShouldRecomputeTotalInterest()
        {
            sut.Execute(model);

            Assert.Equal(TotalInterest, model.TotalInterest);
        }

        [Fact]
        public void TestExecuteShouldAddRecord()
        {
            sut.Execute(model);

            mockLoanRepository.Verify(m => m.Add(It.IsAny<Loan>()), Times.Once());
            mockLoanRepository.Verify(m => m.Add(It.Is<Loan>(m => m.RepaymentAmount == RepaymentAmount)));
            mockLoanRepository.Verify(m => m.Add(It.Is<Loan>(m => m.TotalInterest == TotalInterest)));
            mockUnitOfWork.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void TestExecuteShouldUpdateRecord()
        {
            double expectedRepaymentAmount = 2160.14;
            double expectedTotalInterest = 1843.27;
            model.Id = 1;
            model.Terms = 24;
            model.InterestRate = 3.5;
            model.FinanceAmount = 50000;

            sut.Execute(model);

            mockLoanRepository.Verify(m => m.Get(loan.Id), Times.Once());
            mockLoanRepository.Verify(m => m.Update(It.IsAny<Loan>()), Times.Once());
            mockLoanRepository.Verify(m => m.Update(It.Is<Loan>(m => m.Terms == model.Terms)));
            mockLoanRepository.Verify(m => m.Update(It.Is<Loan>(m => m.InterestRate == model.InterestRate)));
            mockLoanRepository.Verify(m => m.Update(It.Is<Loan>(m => m.FinanceAmount == model.FinanceAmount)));
            mockLoanRepository.Verify(m => m.Update(It.Is<Loan>(m => m.RepaymentAmount == expectedRepaymentAmount)));
            mockLoanRepository.Verify(m => m.Update(It.Is<Loan>(m => m.TotalInterest == expectedTotalInterest)));
            mockUnitOfWork.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
