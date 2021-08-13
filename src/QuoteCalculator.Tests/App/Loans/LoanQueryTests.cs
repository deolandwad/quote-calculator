using AutoMapper;
using Moq;
using QuoteCalculator.App.Loans.Models;
using QuoteCalculator.App.Loans.Queries;
using QuoteCalculator.App.Mapping;
using QuoteCalculator.Data;
using QuoteCalculator.Data.Repositories;
using QuoteCalculator.Domain;
using System.Collections.Generic;
using Xunit;

namespace QuoteCalculator.Tests.App.Loans
{
    public class LoanQueryTests
    {
        private Mock<IRepository<Loan>> mockLoanRepository;
        private Mock<IUnitOfWork> mockUnitOfWork;
        private IMapper mapper;
        private ILoanQuery sut;
        private LoanListModel model;
        private LoanDetailModel detailModel;
        private List<Loan> loans;

        private const int indexRec = 1;

        public LoanQueryTests()
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

            model = new LoanListModel();

            loans = new List<Loan>()
            {
                new Loan()
                {
                    Id = 1,
                    FirstName = "Cammy",
                    LastName = "Albares",
                    Email = "calbares@gmail.com",
                    MobileNumber = "956-537-6195",
                    Terms = 120,
                    InterestRate = 5,
                    FinanceAmount = 20000,
                    RepaymentAmount = 212.13,
                    TotalInterest = 5455.72
                },
                new Loan()
                {
                    Id = 2,
                    FirstName = "Mattie",
                    LastName = "Poquette",
                    Email = "mattie@aol.com",
                    MobileNumber = "602-277-4385",
                    Terms = 24,
                    InterestRate = 3.5,
                    FinanceAmount = 50000,
                    RepaymentAmount = 2160.14,
                    TotalInterest = 1843.27
                }
            };

            var loan = loans[indexRec];
            detailModel = new LoanDetailModel()
            {
                Id = loan.Id
            };

            mockLoanRepository = new Mock<IRepository<Loan>>();
            mockLoanRepository.Setup(m => m.Get(detailModel.Id)).Returns(loan);
            mockLoanRepository.Setup(m => m.All()).Returns(loans);

            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.LoanRepository).Returns(mockLoanRepository.Object);

            sut = new LoanQuery(mockUnitOfWork.Object, mapper);
        }

        [Fact]
        public void TestExecuteShouldReturnListOfLoans()
        {
            var expectedCount = 2;

            sut.Execute(model);

            var results = model.Loans;

            mockLoanRepository.Verify(m => m.All(), Times.Once());
            Assert.Equal(expectedCount, results.Count);

            int index = 0;
            foreach (var loan in loans)
            {
                Assert.Equal(loan.Id, results[index].Id);
                Assert.Equal(loan.FirstName, results[index].FirstName);
                Assert.Equal(loan.LastName, results[index].LastName);
                Assert.Equal(loan.Email, results[index].Email);
                Assert.Equal(loan.MobileNumber, results[index].MobileNumber);
                Assert.Equal(loan.Terms, results[index].Terms);
                Assert.Equal(loan.InterestRate, results[index].InterestRate);
                Assert.Equal(loan.FinanceAmount, results[index].FinanceAmount);
                Assert.Equal(loan.RepaymentAmount, results[index].RepaymentAmount);
                Assert.Equal(loan.TotalInterest, results[index].TotalInterest);
                index++;
            }
        }

        [Fact]
        public void TestExecuteShouldReturnLoanDetail()
        {
            sut.Execute(detailModel);

            mockLoanRepository.Verify(m => m.Get(detailModel.Id), Times.Once());

            Assert.Equal(loans[indexRec].Id, detailModel.Id);
            Assert.Equal(loans[indexRec].FirstName, detailModel.FirstName);
            Assert.Equal(loans[indexRec].LastName, detailModel.LastName);
            Assert.Equal(loans[indexRec].Email, detailModel.Email);
            Assert.Equal(loans[indexRec].MobileNumber, detailModel.MobileNumber);
            Assert.Equal(loans[indexRec].Terms, detailModel.Terms);
            Assert.Equal(loans[indexRec].InterestRate, detailModel.InterestRate);
            Assert.Equal(loans[indexRec].FinanceAmount, detailModel.FinanceAmount);
            Assert.Equal(loans[indexRec].RepaymentAmount, detailModel.RepaymentAmount);
            Assert.Equal(loans[indexRec].TotalInterest, detailModel.TotalInterest);
        }
    }
}
