using AutoMapper;
using LibrarySystem.Dtos.BookDtos;
using LibrarySystem.Entities;

namespace LibrarySystem.Helper
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, GetBookResponse>()
                .ForMember(dest => dest.BookID,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.BookTitle,
                    opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.AuthorName,
                    opt => opt.MapFrom(src => src.Author.Name));

            CreateMap<CreateBookRequest, Book>();
            CreateMap<UpdateBookRequest, Book>();
        }
    }
}
