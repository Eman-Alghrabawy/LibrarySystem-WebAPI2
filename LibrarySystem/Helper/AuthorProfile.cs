using AutoMapper;
using LibrarySystem.Dtos.AuthorDtos;
using LibrarySystem.Entities;

namespace LibrarySystem.Helper
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, GetAuthorResponse>()
                .ForMember(dest => dest.BookTitles,
                    opt => opt.MapFrom(src => src.Books.Select(b => b.Title)));

            CreateMap<CreateAuthorRequest, Author>();
            CreateMap<UpdateAuthorRequest, Author>();
        }
    }
}
