using AutoMapper;
using BookStorePersistence.Domain;

namespace BookStoreService
{
    public class CustomValueResolver : IValueResolver<object, object, string>
    {
        public string Resolve(object source, object destination, string destMember, ResolutionContext context)
        {
            var book = source as Book;
            if (book.Price > 1000)
            {
                return "EXPENSIVE";
            }

            if (book.Price < 100)
            {
                return "CHEAP";
            }

            return "NORMAL";
        }
    }
}
