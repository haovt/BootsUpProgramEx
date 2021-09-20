using BookStoreService.Dto;
using System.Collections.Generic;

namespace BookStoreService
{
    public interface IBSRepository
    {
        IList<BookDto> GetAll();
        void AddBook(BookDto book);
        void UpdateBook(BookDto book);
        void DeleteBook(int bookdId);
        bool IsBookExisted(string title);
    }
}
