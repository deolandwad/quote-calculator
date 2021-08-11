using AutoMapper;
using System.Collections.Generic;
using QuoteCalculator.App.Quotes.Models;
using QuoteCalculator.Data;
using QuoteCalculator.Domain;
using QuoteCalculator.App.Manager;

namespace QuoteCalculator.App.Quotes.Queries
{
    public class QuoteQuery : IQuoteQuery
    {
        public QuoteQuery() { }

        public void Execute(QuoteDetailModel model)
        {
            var loanManager = new LoanManager((double)model.FinanceAmount, (double)model.InterestRate, (int)model.Terms);
            model.RepaymentAmount = loanManager.RepaymentAmount;
            model.QuoteSchedules = loanManager.GetSchedules();
        }
    }
}
