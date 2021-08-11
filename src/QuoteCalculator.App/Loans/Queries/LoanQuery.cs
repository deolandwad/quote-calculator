using AutoMapper;
using System.Collections.Generic;
using QuoteCalculator.App.Loans.Models;
using QuoteCalculator.Data;
using QuoteCalculator.Domain;

namespace QuoteCalculator.App.Loans.Queries
{
    public class LoanQuery : ILoanQuery
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public LoanQuery(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public void Execute(LoanListModel model)
        {
            var loans = unitOfWork.LoanRepository.All();
            model.Loans = mapper.Map<IEnumerable<Loan>, List<LoanDetailModel>>(loans);
        }

        public void Execute(LoanDetailModel model)
        {
            var loan = unitOfWork.LoanRepository.Get(model.Id);
            mapper.Map(loan, model);
        }

        public void Execute(EditLoanModel model)
        {
            var loan = unitOfWork.LoanRepository.Get(model.Id);
            mapper.Map(loan, model);
        }
    }
}
