namespace QuoteCalculator.Domain
{
    public class Loan
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public int Terms { get; set; }
        public double InterestRate { get; set; }
        public double FinanceAmount { get; set; }
        public double RepaymentAmount { get; set; }
        public double TotalInterest { get; set; }
    }
}
