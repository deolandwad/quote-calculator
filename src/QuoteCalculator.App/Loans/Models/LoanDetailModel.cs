namespace QuoteCalculator.App.Loans.Models
{
    public class LoanDetailModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public int Terms { get; set; }
        public double InterestRate { get; set; }
        public double FinanceAmount { get; set; }
        public string FullName { get; set; }
        public double RepaymentAmount { get; set; }
    }
}
