using AutoMapper;
using QuoteCalculator.App.Loans.Models;
using QuoteCalculator.App.Quotes.Models;
using QuoteCalculator.Domain;

namespace QuoteCalculator.App.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            QuoteMappingProfile();
            LoanMappingProfile();
        }

        private void QuoteMappingProfile()
        {
            CreateMap<QuoteDetailModel, Loan>()
                .ForSourceMember(src => src.FullName, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.QuoteSchedules, opt => opt.DoNotValidate());
        }

        private void LoanMappingProfile()
        {
            CreateMap<Loan, LoanDetailModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));

            CreateMap<Loan, EditLoanModel>()
                .ReverseMap();
        }
    }
}
