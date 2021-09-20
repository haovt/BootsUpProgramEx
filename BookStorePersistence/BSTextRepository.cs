using BookStorePersistence.Domain;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookStorePersistence
{
    public class BSTextRepository : BSRepository, IBSRepository
    {
        protected override string filePath => @"D:\BootsUpProgramEx\BookStorePersistence\BooksData.txt";

        protected override void WriteToFile()
        {
            var bookdData = BookDataValue.Select(x => x.ToString());
            File.WriteAllLines(filePath, bookdData);
        }

        protected override IList<Book> LoadData()
        {
            var result = new List<string>();

            using (StreamReader file = new StreamReader(filePath))
            {
                var line = file.ReadLine();
                while (line != null)
                {
                    result.Add(line);
                    line = file.ReadLine();
                }
            }

            return result.Select(x => ConvertToBook(x)).ToList();
        }

        private Book ConvertToBook(string book)
        {
            var bookDetail = book.Split(',');
            return new Book
            {
                Id = int.Parse(bookDetail[0]),
                Title = bookDetail[1],
                Author = bookDetail[2],
                Price = int.Parse(bookDetail[3])
            };
        }
    }
}
