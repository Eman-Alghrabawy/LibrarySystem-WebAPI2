using AutoMapper;
using LibrarySystem.Dtos.BorrowerDtos;
using LibrarySystem.Entities;

namespace LibrarySystem.Helper
{
    public class BorrowerProfile : Profile
    {
        public BorrowerProfile()
        {
            CreateMap<Borrower, GetBorrowerResponse>()
                .ForMember(dest => dest.BorrowedBookTitles,
                    opt => opt.MapFrom(src => src.Loans.Select(l => l.Book.Title)));

            CreateMap<CreateBorrowerRequest, Borrower>();
            CreateMap<UpdateBorrowerRequest, Borrower>();
        }
    }
}
