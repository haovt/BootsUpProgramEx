using AutoMapper;
using BookStorePersistence;
using BookStorePersistence.Domain;
using BookStoreService.Dto;
using Ninject;
using System.Collections.Generic;

namespace BookStoreService
{
    // HTTP Error 500.0 - ANCM In-Process Handler Load Failure
    // for .NET Framework
    public class BSService : IBSService
    {
        private readonly IBSRepository _bsTextRepository;
        private readonly IBSRepository _bsJsonRepository;
        private readonly IMapper _mapper;

        public BSService([Named("TextRepo")] IBSRepository bsTextRepository,
            [Named("JsonRepo")] IBSRepository bsJsonRepository)
        {
            _bsTextRepository = bsTextRepository;
            _bsJsonRepository = bsJsonRepository;

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BookMappingProfile());
            }).CreateMapper();
        }

        public IList<BookDto> GetAll(FileMode mode = FileMode.Text)
        {
            if (mode == FileMode.Text)
            {
                return _mapper.Map<IList<BookDto>>(_bsTextRepository.GetAll());
            }

            return _mapper.Map<IList<BookDto>>(_bsJsonRepository.GetAll());
        }

        public void AddBook(BookDto bookDto, FileMode mode = FileMode.Text)
        {
            if (mode == FileMode.Text)
            {
                _bsTextRepository.AddBook(_mapper.Map<Book>(bookDto));
            }
            else
            {
                _bsJsonRepository.AddBook(_mapper.Map<Book>(bookDto));
            }
        }

        public void UpdateBook(BookDto bookDto, FileMode mode = FileMode.Text)
        {
            if (mode == FileMode.Text)
            {
                _bsTextRepository.UpdateBook(_mapper.Map<Book>(bookDto));
            }
            else
            {
                _bsJsonRepository.UpdateBook(_mapper.Map<Book>(bookDto));
            }
        }

        public void DeleteBook(int bookdId, FileMode mode = FileMode.Text)
        {
            if (mode == FileMode.Text)
            {
                _bsTextRepository.DeleteBook(bookdId);
            }
            else
            {
                _bsJsonRepository.DeleteBook(bookdId);
            }
        }
    }

    public enum FileMode
    {
        Text = 1,
        Json
    }
}
