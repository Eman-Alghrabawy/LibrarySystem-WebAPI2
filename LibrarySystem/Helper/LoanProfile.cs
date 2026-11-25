using AutoMapper;
using LibrarySystem.Dtos.LoanDtos;
using LibrarySystem.Entities;

namespace LibrarySystem.Helper
{
    public class LoanProfile : Profile
    {
        public LoanProfile()
        {
            CreateMap<Loan, GetLoanResponse>()
                .ForMember(dest => dest.BookTitle,
                    opt => opt.MapFrom(src => src.Book.Title))
                .ForMember(dest => dest.BorrowerName,
                    opt => opt.MapFrom(src => src.Borrower.Name));

            CreateMap<CreateLoanRequest, Loan>();
            CreateMap<UpdateLoanRequest, Loan>();
        }
    }
}
