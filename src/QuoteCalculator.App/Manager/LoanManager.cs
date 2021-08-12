using QuoteCalculator.App.Quotes.Models;
using System;
using System.Collections.Generic;

namespace QuoteCalculator.App.Manager
{
    public class LoanManager
    {
        private readonly double amount;
        private readonly double interest;
        private readonly int terms;
        private readonly int frequency;
        private double repaymentAmount;
        private double totalInterest;

        public LoanManager(double amount, double interest, int terms)
        {
            if (amount < 0) throw new ArgumentException($"{nameof(amount)} cannot be {amount}");
            if (interest < 0) throw new ArgumentException($"{nameof(interest)} cannot be {interest}");
            if (terms <= 0) throw new ArgumentException($"{nameof(terms)} cannot be {terms}");

            frequency = 12; // Number of payment per year for monthly. TODO: Allow other frequency.
            this.amount = amount;
            this.interest = interest / 100 / frequency;
            this.terms = terms;

            ComputePayment();
            ComputeTotalInterest();
        }

        public double RepaymentAmount { get { return Math.Round(repaymentAmount, 2); } }
        public double TotalInterest { get { return Math.Round(totalInterest, 2); } }

        public List<QuoteScheduleModel> GetSchedules()
        {
            List<QuoteScheduleModel> quoteSchedules = new List<QuoteScheduleModel>();
            double balance = amount;

            for (int payNo = 1; payNo <= terms; payNo++)
            {
                var quoteSchedule = new QuoteScheduleModel();

                quoteSchedule.PaymentNo = payNo;
                quoteSchedule.Payment = repaymentAmount;
                quoteSchedule.Principal = ComputePrincipal(balance);
                quoteSchedule.Interest = ComputeInterest(balance);
                balance -= ComputePrincipal(balance);
                quoteSchedule.Balance = balance;

                quoteSchedules.Add(quoteSchedule);
            }

            return quoteSchedules;
        }

        private void ComputePayment()
        {
            // Amount * AnnualInterestRate / PaymentsPerYear *
            // (1 + AnnualInterestRate / PaymentsPerYear) ^ (Years * PaymentsPerYear) /
            // ((1 + AnnualInterestRate / PaymentsPerYear) ^ (Years * PaymentsPerYear) - 1)

            repaymentAmount = amount * interest * Math.Pow((1.0 + interest), terms) / (Math.Pow((1.0 + interest), terms) - 1.0);
        }

        private double ComputePrincipal(double balance)
        {
            // Principal = Payment - Interest

            return repaymentAmount - ComputeInterest(balance);
        }

        private double ComputeInterest(double balance)
        {
            // Interest = (5%/12) x 20,000

            return interest * balance;
        }

        private void ComputeTotalInterest()
        {
            double balance = amount;

            for (int payNo = 1; payNo <= terms; payNo++)
            {
                totalInterest += ComputeInterest(balance);
                balance -= ComputePrincipal(balance);
            }
        }

    }
}
