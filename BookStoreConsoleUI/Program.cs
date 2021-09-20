using BookStoreService;
using BookStoreService.Dto;
using log4net;
using log4net.Config;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStoreConsoleUI
{
    class Program
    {
        private static IBSRepository _service;
        private static ReadFileMode fileMode = ReadFileMode.Text;
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            BasicConfigurator.Configure();

            IKernel kernel = new StandardKernel(new ServiceModule());

            Console.WriteLine("Book Store app started! Please select file mode:");
            Console.WriteLine("1 - Text");
            Console.WriteLine("2 - Json");
            string modeText = Console.ReadLine();
            int mode = Convert.ToInt32(modeText);
            fileMode = (ReadFileMode)mode;

            _service = fileMode == ReadFileMode.Text ? kernel.Get<IBSRepository>("TextRepo")
                : kernel.Get<IBSRepository>("JsonRepo");

            Console.WriteLine("Below are all books in store:");
            var books = _service.GetAll().ToList();
            RefreshScreen(books);

            Console.WriteLine("Please select action:");
            Console.WriteLine("1 - Insert a new book");
            Console.WriteLine("2 - Update book");
            Console.WriteLine("3 - Delete book");

            string val = Console.ReadLine();
            int action = Convert.ToInt32(val);
            switch (action)
            {
                case 1:
                    InsertBookAction();
                    break;
                case 2:
                    UpdateBookAction(books);
                    break;
                case 3:
                    DeleteBookAction();
                    break;
                default:
                    Console.WriteLine("Invalid action");
                    break;
            };
        }

        private static void RefreshScreen(List<BookDto> books = null)
        {
            if (books == null || !books.Any())
            {
                log.Info($"GetAll(): get all Books from {fileMode} mode");
                books = _service.GetAll().ToList();
            }

            foreach (var book in books)
            {
                Console.WriteLine($"{book.Id}. {book.Title} - Author: {book.Author} - Price: {book.Price}");
            }
        }

        private static void InsertBookAction()
        {
            Console.WriteLine("Please enter Book detail:");
            Console.WriteLine("Title:");
            string title = Console.ReadLine();

            Console.WriteLine("Author:");
            string author = Console.ReadLine();

            Console.WriteLine("Price:");
            string priceText = Console.ReadLine();
            int price = Convert.ToInt32(priceText);

            var newBook = new BookDto
            {
                Author = author,
                Title = title,
                Price = price
            };

            log.Info($"Insert new book: Title={title}, Author={author}, Price={price}");
            _service.AddBook(newBook);
            RefreshScreen();
        }

        private static void UpdateBookAction(List<BookDto> books)
        {
            Console.WriteLine("Please select Book's Id to update:");
            string bookId = Console.ReadLine();
            int id = Convert.ToInt32(bookId);

            var book = books.Single(x => x.Id == id);

            Console.WriteLine("Please select field to update:");
            Console.WriteLine("1 - Title");
            Console.WriteLine("2 - Author");
            Console.WriteLine("3 - Price");

            string field = Console.ReadLine();
            switch (field)
            {
                case "1":
                    Console.WriteLine("Enter new value");
                    string title = Console.ReadLine();
                    var oldTitle = book.Title;
                    book.Title = title;
                    log.Info($"Update book's title from {oldTitle} to {title}");
                    _service.UpdateBook(book);
                    RefreshScreen();
                    break;
                case "2":
                    Console.WriteLine("Enter new value");
                    string author = Console.ReadLine();
                    var oldAuthor = book.Author;
                    book.Author = author;
                    log.Info($"Update book's author from {oldAuthor} to {author}");
                    _service.UpdateBook(book);
                    RefreshScreen();
                    break;
                case "3":
                    Console.WriteLine("Enter new value");
                    string priceText = Console.ReadLine();
                    int price = Convert.ToInt32(priceText);
                    var oldPrice = book.Price;
                    book.Price = price;
                    log.Info($"Update book's price from {oldPrice} to {price}");
                    _service.UpdateBook(book);
                    RefreshScreen();
                    break;
                default:
                    Console.WriteLine("Invalid action");
                    break;
            }

        }

        private static void DeleteBookAction()
        {
            Console.WriteLine("Please enter Book's Id to delete:");
            string bookId = Console.ReadLine();
            int id = Convert.ToInt32(bookId);
            log.Info($"Delete book with Id={id}");
            _service.DeleteBook(id);
            RefreshScreen();
        }
    }
}
