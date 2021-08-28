using BookStoreService;
using BookStoreService.Dto;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStoreConsoleUI
{
    class Program
    {
        private static IBSService _service;
        private static FileMode fileMode = FileMode.Text;

        static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel(new ServiceModule());
            _service = kernel.Get<IBSService>();

            Console.WriteLine("Book Store app started! Please select file mode:");
            Console.WriteLine("1 - Text");
            Console.WriteLine("2 - Json");
            string modeText = Console.ReadLine();
            int mode = Convert.ToInt32(modeText);
            fileMode = (FileMode)mode;

            Console.WriteLine("Below are all books in store:");
            var books = _service.GetAll(fileMode).ToList();
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
                books = _service.GetAll(fileMode).ToList();
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

            _service.AddBook(newBook, fileMode);
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
                    book.Title = title;
                    _service.UpdateBook(book, fileMode);
                    RefreshScreen();
                    break;
                case "2":
                    Console.WriteLine("Enter new value");
                    string author = Console.ReadLine();
                    book.Title = author;
                    _service.UpdateBook(book, fileMode);
                    RefreshScreen();
                    break;
                case "3":
                    Console.WriteLine("Enter new value");
                    string priceText = Console.ReadLine();
                    int price = Convert.ToInt32(priceText);
                    book.Price = price;
                    _service.UpdateBook(book, fileMode);
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

            _service.DeleteBook(id, fileMode);
            RefreshScreen();
        }
    }
}
