using QuoteCalculator.Domain;
using Xunit;

namespace QuoteCalculator.UnitTests.Domain
{
    public class LoanTests
    {
        private readonly Loan loan;
        private const int Id = 1;
        private const string FirstName = "Cammy";
        private const string LastName = "Albares";
        private const string Email = "calbares@gmail.com";
        private const string MobileNumber = "956-537-6195";
        private const int Terms = 120;
        private const double InterestRate = 5;
        private const double FinanceAmount = 20000;
        private const double RepaymentAmount = 212.13;

        public LoanTests()
        {
            loan = new Loan();
        }

        [Fact]
        public void TestSetAndGetId()
        {
            loan.Id = Id;

            Assert.Equal(loan.Id, Id);
        }

        [Fact]
        public void TestSetAndGetFirstName()
        {
            loan.FirstName = FirstName;

            Assert.Equal(loan.FirstName, FirstName);
        }

        [Fact]
        public void TestSetAndGetLastName()
        {
            loan.LastName = LastName;

            Assert.Equal(loan.LastName, LastName);
        }

        [Fact]
        public void TestSetAndGetEmail()
        {
            loan.Email = Email;

            Assert.Equal(loan.Email, Email);
        }

        [Fact]
        public void TestSetAndGetMobileNumber()
        {
            loan.MobileNumber = MobileNumber;

            Assert.Equal(loan.MobileNumber, MobileNumber);
        }

        [Fact]
        public void TestSetAndGetTerms()
        {
            loan.Terms = Terms;

            Assert.Equal(loan.Terms, Terms);
        }

        [Fact]
        public void TestSetAndGetInterestRate()
        {
            loan.InterestRate = InterestRate;

            Assert.Equal(loan.InterestRate, InterestRate);
        }

        [Fact]
        public void TestSetAndGetFinanceAmount()
        {
            loan.FinanceAmount = FinanceAmount;

            Assert.Equal(loan.FinanceAmount, FinanceAmount);
        }

        [Fact]
        public void TestSetAndGetRepaymentAmount()
        {
            loan.RepaymentAmount = RepaymentAmount;

            Assert.Equal(loan.RepaymentAmount, RepaymentAmount);
        }
    }
}