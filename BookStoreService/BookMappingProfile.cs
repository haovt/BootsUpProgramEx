using AutoMapper;
using BookStorePersistence.Domain;
using BookStoreService.Dto;

namespace BookStoreService
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            this.CreateMap<BookDto, Book>().ReverseMap();
        }
    }
}
