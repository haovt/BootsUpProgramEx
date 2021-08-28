namespace BookStoreService.Dto
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Price { get; set; }

        public BookDto Copy()
        {
            return (BookDto)MemberwiseClone();
        }
    }
}
