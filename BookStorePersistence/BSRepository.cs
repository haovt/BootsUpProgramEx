using BookStorePersistence.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStorePersistence
{
    public abstract class BSRepository
    {
        protected virtual string filePath => "";

        private IList<Book> BookStore
        {
            get
            {
                if (BookDataValue.Any())
                {
                    return BookDataValue;
                }

                BookDataValue = InitCache();
                return BookDataValue;
            }
        }

        protected IList<Book> BookDataValue { get; set; } = new List<Book>();

        public IList<Book> GetAll()
        {
            return BookStore;
        }

        public bool ExistBook(string title)
        {
            return BookStore.Any(x => x.Title == title);
        }

        public void AddBook(Book book)
        {
            book.Id = BookStore.Any() ? BookStore.Max(x => x.Id) + 1 : 1;
            BookStore.Add(book);
            WriteToFile();
        }

        public void UpdateBook(Book book)
        {
            var bookToUpdate = BookStore.SingleOrDefault(x => x.Id == book.Id);
            if (bookToUpdate != null)
            {
                bookToUpdate.Title = book.Title;
                bookToUpdate.Author = book.Author;
                bookToUpdate.Price = book.Price;

                WriteToFile();
            }
            else
            {
                throw new Exception("Book not found");
            }
        }

        public void DeleteBook(int bookdId)
        {
            var bookToDelete = BookStore.SingleOrDefault(x => x.Id == bookdId);
            if (bookToDelete != null)
            {
                BookStore.Remove(bookToDelete);
                WriteToFile();
            }
            else
            {
                throw new Exception("Book not found");
            }
        }

        private IList<Book> InitCache()
        {
            return LoadData();
        }

        protected virtual void WriteToFile()
        {
            // To be overriden
        }

        protected virtual IList<Book> LoadData()
        {
            // To be overriden
            return null;
        }
    }
}
