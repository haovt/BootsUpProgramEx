using AutoMapper;
using BookStorePersistence;
using BookStorePersistence.Domain;
using BookStoreService.Dto;
using Ninject;
using System;
using System.Collections.Generic;

namespace BookStoreService
{
    // HTTP Error 500.0 - ANCM In-Process Handler Load Failure
    // for .NET Framework
    public class BSService : IBSRepository
    {
        private readonly BookStorePersistence.IBSRepository _bsRepository;
        private readonly IMapper _mapper;

        public BSService(BookStorePersistence.IBSRepository bsRepository)
        {
            _bsRepository = bsRepository;

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BookMappingProfile());
            }).CreateMapper();
        }

        public IList<BookDto> GetAll()
        {
            return _mapper.Map<IList<BookDto>>(_bsRepository.GetAll());
        }

        public void AddBook(BookDto bookDto)
        {
            if (IsBookExisted(bookDto.Title))
            {
                throw new Exception("Book existed. Please try again");
            }

            _bsRepository.AddBook(_mapper.Map<Book>(bookDto));
        }

        public void UpdateBook(BookDto bookDto)
        {
            _bsRepository.UpdateBook(_mapper.Map<Book>(bookDto));
        }

        public void DeleteBook(int bookdId)
        {
            _bsRepository.DeleteBook(bookdId);
        }

        public bool IsBookExisted(string title)
        {
            return _bsRepository.ExistBook(title);
        }
    }

    public enum ReadFileMode
    {
        Text = 1,
        Json
    }
}
