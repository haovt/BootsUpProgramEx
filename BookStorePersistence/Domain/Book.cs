namespace BookStorePersistence.Domain
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Price { get; set; }

        public override string ToString()
        {
            return $"{Id},{Title},{Author},{Price}";
        }
    }
}
