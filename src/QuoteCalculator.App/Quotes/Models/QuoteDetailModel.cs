using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuoteCalculator.App.Quotes.Models
{
    public class QuoteDetailModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "Please enter valid Mobile number")]
        [Phone(ErrorMessage = "Please enter valid Mobile number")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Terms is required")]
        [Range(1, 480, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public int? Terms { get; set; }

        [Required(ErrorMessage = "Interest Rate is required")]
        [Range(1.0, 50.0, ErrorMessage = "Value for Interest Rate must be between {1} and {2}")]
        public double? InterestRate { get; set; }

        [Required(ErrorMessage = "Loan Amount is required")]
        [Range(2000.0, 50000.0, ErrorMessage = "Value for Loan Amount must be between {1} and {2}")]
        public double? FinanceAmount { get; set; }

        public double RepaymentAmount { get; set; }

        public double TotalInterest { get; set; }

        public string FullName { get { return FirstName + " " + LastName; } }

        public List<QuoteScheduleModel> QuoteSchedules { get; set; } = new List<QuoteScheduleModel>();
    }
}
