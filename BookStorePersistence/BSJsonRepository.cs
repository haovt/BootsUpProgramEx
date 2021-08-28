using BookStorePersistence.Domain;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace BookStorePersistence
{
    public class BSJsonRepository : BSRepository, IBSRepository
    {
        protected override string filePath => @"D:\Boots_Up_Program\Exampleprj\BookStorePersistence\BooksData.json";

        protected override void WriteToFile()
        {
            var json = JsonConvert.SerializeObject(BookDataValue);
            File.WriteAllText(filePath, json);
        }

        protected override IList<Book> LoadData()
        {
            var result = new List<Book>();
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                result = JsonConvert.DeserializeObject<List<Book>>(json);
            }

            return result;
        }
    }
}
