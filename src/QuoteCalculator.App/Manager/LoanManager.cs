using Microsoft.VisualBasic;
using QuoteCalculator.App.Quotes.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteCalculator.App.Manager
{
    public class LoanManager
    {
        private readonly double amount;
        private readonly double interest;
        private readonly int terms;
        private readonly int frequency;
        private readonly double repaymentAmount;

        public LoanManager(double amount, double interest, int terms)
        {
            this.frequency = 12; // Number of payment per year for monthly. TODO: Allow other frequency.
            this.amount = amount;
            this.interest = interest / 100 / frequency;
            this.terms = terms;
            this.repaymentAmount = ComputePayment(this.interest, this.terms, this.amount);
        }

        public double RepaymentAmount { get { return Math.Round(repaymentAmount, 2); } }

        public List<QuoteScheduleModel> GetSchedules()
        {
            List<QuoteScheduleModel> quoteSchedules = new List<QuoteScheduleModel>();
            double balance = amount;

            for (int payNo = 1; payNo <= terms; payNo++)
            {
                var quoteSchedule = new QuoteScheduleModel();
                quoteSchedule.PaymentNo = payNo;
                quoteSchedule.Payment = repaymentAmount;
                quoteSchedule.Principal = ComputePrincipal(repaymentAmount, interest, balance);
                quoteSchedule.Interest = ComputeInterest(interest, balance);
                balance -= ComputePrincipal(repaymentAmount, interest, balance);

                quoteSchedule.Balance = balance;
                quoteSchedules.Add(quoteSchedule);
            }

            return quoteSchedules;
        }

        private double ComputePayment(double interest, int terms, double amount)
        {
            // Amount * AnnualInterestRate / PaymentsPerYear *
            // (1 + AnnualInterestRate / PaymentsPerYear) ^ (Years * PaymentsPerYear) /
            // ((1 + AnnualInterestRate / PaymentsPerYear) ^ (Years * PaymentsPerYear) - 1)

            double payment = amount * interest * Math.Pow((1.0 + interest), terms) / (Math.Pow((1.0 + interest), terms) - 1.0);

            return payment;
        }

        private double ComputePrincipal(double payment, double interest, double amount)
        {
            // Principal = Payment - Interest
            
            return payment - ComputeInterest(interest, amount);
        }

        private double ComputeInterest(double interest, double amount)
        {
            // Interest = (5%/12) x 20,000

            return interest * amount;
        }
    }
}
