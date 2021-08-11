using AutoMapper;
using QuoteCalculator.App.Loans.Models;
using QuoteCalculator.App.Manager;
using QuoteCalculator.Data;
using QuoteCalculator.Domain;

namespace QuoteCalculator.App.Loans.Commands
{
    public class LoanCommand : ILoanCommand
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public LoanCommand(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public void Execute(EditLoanModel model)
        {
            var loanManager = new LoanManager((double)model.FinanceAmount, (double)model.InterestRate, (int)model.Terms);
            model.RepaymentAmount = loanManager.RepaymentAmount;

            if (model.Id == 0) // New Loan
            {
                var loan = mapper.Map<EditLoanModel, Loan>(model);
                unitOfWork.LoanRepository.Add(loan);
            }
            else
            {
                var loan = unitOfWork.LoanRepository.Get(model.Id);
                mapper.Map(model, loan);
                unitOfWork.LoanRepository.Update(loan);
            }

            unitOfWork.SaveChanges();
        }

        public void Execute(DeleteLoanModel model)
        {
            var loan = unitOfWork.LoanRepository.Get(model.Id);
            unitOfWork.LoanRepository.Remove(loan);
            unitOfWork.SaveChanges();
        }
    }
}
