using BookStoreService.Dto;
using System.Collections.Generic;

namespace BookStoreService
{
    public interface IBSService
    {
        IList<BookDto> GetAll(FileMode mode = FileMode.Text);
        void AddBook(BookDto book, FileMode mode = FileMode.Text);
        void UpdateBook(BookDto book, FileMode mode = FileMode.Text);
        void DeleteBook(int bookdId, FileMode mode = FileMode.Text);
    }
}
