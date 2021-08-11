using System.Collections.Generic;

namespace QuoteCalculator.App.Loans.Models
{
    public class LoanListModel
    {
        public List<LoanDetailModel> Loans { get; set; } = new List<LoanDetailModel>();
    }
}
