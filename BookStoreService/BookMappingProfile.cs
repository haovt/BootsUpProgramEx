using AutoMapper;
using BookStorePersistence.Domain;
using BookStoreService.Dto;

namespace BookStoreService
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            this.CreateMap<Book, BookDto>()
                .ForMember(desc => desc.Level, opt => opt.MapFrom(new CustomValueResolver()));
            this.CreateMap<BookDto, Book>();
        }
    }
}
