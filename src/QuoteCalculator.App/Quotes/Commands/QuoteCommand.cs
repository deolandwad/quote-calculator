using AutoMapper;
using QuoteCalculator.App.Quotes.Models;
using QuoteCalculator.Data;
using QuoteCalculator.Domain;

namespace QuoteCalculator.App.Quotes.Commands
{
    public class QuoteCommand : IQuoteCommand
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public QuoteCommand(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public void Execute(QuoteDetailModel model)
        {
            if (model.Id == 0) // New Loan
            {
                var loan = mapper.Map<QuoteDetailModel, Loan>(model);
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
    }
}
