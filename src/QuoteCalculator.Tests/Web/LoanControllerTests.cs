using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using QuoteCalculator.App.Loans.Commands;
using QuoteCalculator.App.Loans.Models;
using QuoteCalculator.App.Loans.Queries;
using QuoteCalculator.App.Mapping;
using QuoteCalculator.Data;
using QuoteCalculator.Data.Repositories;
using QuoteCalculator.Domain;
using System.Collections.Generic;
using WTWMiniApp.Web.Areas.Contacts.Controllers;
using Xunit;

namespace QuoteCalculator.Tests.Web
{
    public class LoanControllerTests
    {
        private Mock<IRepository<Loan>> mockLoanRepository;
        private Mock<IUnitOfWork> mockUnitOfWork;
        private IMapper mapper;
        private LoanCommand loanCommand;
        private LoanQuery loanQuery;
        private LoanController sut;
        private LoanListModel model;
        private LoanDetailModel detailModel;
        private EditLoanModel editModel;
        private List<Loan> loans;

        private DefaultHttpContext httpContext;
        private TempDataDictionary tempData;

        private const int indexRec = 1;
        private const string Success = "Loan has been saved.";
        private const string Failure = "Unable to save loan. Please try again.";

        public LoanControllerTests()
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

            loanCommand = new LoanCommand(mockUnitOfWork.Object, mapper);
            loanQuery = new LoanQuery(mockUnitOfWork.Object, mapper);

            httpContext = new DefaultHttpContext();
            tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            sut = new LoanController(loanCommand, loanQuery);
        }

        [Fact]
        public void TestGetListShouldReturnListOfLoans()
        {
            var expectedCount = 2;

            ViewResult viewResult = (ViewResult)sut.List(model);
            var viewModel = (LoanListModel)viewResult.Model;
            var results = viewModel.Loans;

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
        public void TestGetDetailShouldReturnLoanDetail()
        {
            var actionResult = sut.Detail(detailModel);
            var viewResult = Assert.IsType<ViewResult>(actionResult);
            var viewModel = Assert.IsType<LoanDetailModel>(viewResult.Model);

            mockLoanRepository.Verify(m => m.Get(viewModel.Id), Times.Once());

            Assert.Equal(loans[indexRec].Id, viewModel.Id);
            Assert.Equal(loans[indexRec].FirstName, viewModel.FirstName);
            Assert.Equal(loans[indexRec].LastName, viewModel.LastName);
            Assert.Equal(loans[indexRec].Email, viewModel.Email);
            Assert.Equal(loans[indexRec].MobileNumber, viewModel.MobileNumber);
            Assert.Equal(loans[indexRec].Terms, viewModel.Terms);
            Assert.Equal(loans[indexRec].InterestRate, viewModel.InterestRate);
            Assert.Equal(loans[indexRec].FinanceAmount, viewModel.FinanceAmount);
            Assert.Equal(loans[indexRec].RepaymentAmount, viewModel.RepaymentAmount);
            Assert.Equal(loans[indexRec].TotalInterest, viewModel.TotalInterest);
        }

        [Fact]
        public void TestPostCreateShouldExecuteCreateLoanCommand()
        {
            var loan = loans[indexRec];
            editModel = new EditLoanModel()
            {
                Id = 0,
                FirstName = loan.FirstName,
                LastName = loan.LastName,
                Email = loan.Email,
                MobileNumber = loan.MobileNumber,
                Terms = loan.Terms,
                InterestRate = loan.InterestRate,
                FinanceAmount = loan.FinanceAmount
            };
            tempData.Add("Success", Success);
            sut.TempData = tempData;

            var actionResult = sut.Edit(editModel);

            Assert.Equal(Success, sut.Success);
            mockLoanRepository.Verify(m => m.Add(It.IsAny<Loan>()), Times.Once());
            mockLoanRepository.Verify(m => m.Add(It.Is<Loan>(m => m.RepaymentAmount == loan.RepaymentAmount)));
            mockLoanRepository.Verify(m => m.Add(It.Is<Loan>(m => m.TotalInterest == loan.TotalInterest)));
            mockUnitOfWork.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void TestPostCreateShouldValidateModelState()
        {
            var loan = loans[indexRec];
            editModel = new EditLoanModel()
            {
                Id = 0,
                FirstName = loan.FirstName,
                LastName = loan.LastName,
                Email = loan.Email,
                MobileNumber = loan.MobileNumber,
            };
            tempData.Add("Failure", Failure);
            sut.TempData = tempData;
            sut.ModelState.AddModelError("Terms", "Terms is required");
            sut.ModelState.AddModelError("InterestRate", "Interest Rate is required");
            sut.ModelState.AddModelError("FinanceAmount", "Loan Amount is required");

            var actionResult = sut.Edit(editModel);

            Assert.Equal(Failure, sut.Failure);
            mockLoanRepository.Verify(m => m.Add(It.IsAny<Loan>()), Times.Never());
            mockUnitOfWork.Verify(m => m.SaveChanges(), Times.Never());
        }

        [Fact]
        public void TestPostUpdateShouldExecuteUpdateLoanCommand()
        {
            var loan = loans[indexRec];
            editModel = new EditLoanModel()
            {
                Id = loan.Id,
                FirstName = loan.FirstName,
                LastName = loan.LastName,
                Email = loan.Email,
                MobileNumber = loan.MobileNumber,
                Terms = loan.Terms,
                InterestRate = loan.InterestRate,
                FinanceAmount = loan.FinanceAmount
            };
            tempData.Add("Success", Success);
            sut.TempData = tempData;

            var actionResult = sut.Edit(editModel);

            Assert.Equal(Success, sut.Success);
            mockLoanRepository.Verify(m => m.Update(It.IsAny<Loan>()), Times.Once());
            mockLoanRepository.Verify(m => m.Update(It.Is<Loan>(m => m.Id == loan.Id)));
            mockLoanRepository.Verify(m => m.Update(It.Is<Loan>(m => m.FirstName == loan.FirstName)));
            mockLoanRepository.Verify(m => m.Update(It.Is<Loan>(m => m.LastName == loan.LastName)));
            mockLoanRepository.Verify(m => m.Update(It.Is<Loan>(m => m.Email == loan.Email)));
            mockLoanRepository.Verify(m => m.Update(It.Is<Loan>(m => m.MobileNumber == loan.MobileNumber)));
            mockLoanRepository.Verify(m => m.Update(It.Is<Loan>(m => m.Terms == loan.Terms)));
            mockLoanRepository.Verify(m => m.Update(It.Is<Loan>(m => m.InterestRate == loan.InterestRate)));
            mockLoanRepository.Verify(m => m.Update(It.Is<Loan>(m => m.FinanceAmount == loan.FinanceAmount)));
            mockLoanRepository.Verify(m => m.Update(It.Is<Loan>(m => m.RepaymentAmount == loan.RepaymentAmount)));
            mockLoanRepository.Verify(m => m.Update(It.Is<Loan>(m => m.TotalInterest == loan.TotalInterest)));
            mockUnitOfWork.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void TestPostUpdateShouldValidateModelState()
        {
            var loan = loans[indexRec];
            editModel = new EditLoanModel()
            {
                Id = loan.Id,
                FirstName = loan.FirstName,
                LastName = loan.LastName,
                Email = loan.Email,
                MobileNumber = loan.MobileNumber,
            };
            tempData.Add("Failure", Failure);
            sut.TempData = tempData;
            sut.ModelState.AddModelError("Terms", "Terms is required");
            sut.ModelState.AddModelError("InterestRate", "Interest Rate is required");
            sut.ModelState.AddModelError("FinanceAmount", "Loan Amount is required");

            var actionResult = sut.Edit(editModel);

            Assert.Equal(Failure, sut.Failure);
            mockLoanRepository.Verify(m => m.Add(It.IsAny<Loan>()), Times.Never());
            mockUnitOfWork.Verify(m => m.SaveChanges(), Times.Never());
        }
    }
}
