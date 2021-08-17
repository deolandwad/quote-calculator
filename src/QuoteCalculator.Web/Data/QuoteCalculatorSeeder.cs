using Microsoft.AspNetCore.Hosting;
using QuoteCalculator.App.Manager;
using QuoteCalculator.Data;
using QuoteCalculator.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace QuoteCalculator.Web.Data
{
    public class QuoteCalculatorSeeder
    {
        private readonly QuoteCalculatorContext context;
        private readonly IWebHostEnvironment hosting;
        private readonly IUnitOfWork unitOfWork;
        private readonly Random random = new Random();

        public QuoteCalculatorSeeder(QuoteCalculatorContext context, IWebHostEnvironment hosting)
        {
            this.context = context;
            this.hosting = hosting;
            unitOfWork = new QuoteCalculatorUnitOfWork(context);
        }

        public void Seed()
        {
            context.Database.EnsureCreated();

            if (!unitOfWork.LoanRepository.All().Any())
            {
                var file = Path.Combine(hosting.ContentRootPath, "Data/quote.json");
                var json = File.ReadAllText(file);
                var loanList = JsonSerializer.Deserialize<IEnumerable<Loan>>(json);

                foreach (var loan in loanList)
                {
                    loan.FinanceAmount = random.Next(5000, 50000);
                    loan.InterestRate = random.Next(3, 10);
                    loan.Terms = random.Next(24, 120);

                    AddLoan(loan);
                }

                //throw new SystemException();

                unitOfWork.SaveChanges();
            }
        }

        private void AddLoan(Loan loan)
        {
            // Compute RepaymentAmount and TotalInterest
            var loanManager = new LoanManager(loan.FinanceAmount, loan.InterestRate, loan.Terms);
            loan.RepaymentAmount = loanManager.RepaymentAmount;
            loan.TotalInterest = loanManager.TotalInterest;

            unitOfWork.LoanRepository.Add(loan);
        }
    }
}