using BookStorePersistence.Domain;
using System.Collections.Generic;

namespace BookStorePersistence
{
    public interface IBSRepository
    {
        IList<Book> GetAll();
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(int bookdId);
        bool ExistBook(string title);
    }
}
