using BookStorePersistence;
using Ninject.Modules;

namespace BookStoreService
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBSRepository>().To<BSService>().Named("TextRepo");
            Bind<IBSRepository>().To<BSService>().Named("JsonRepo");
            Bind<BookStorePersistence.IBSRepository>().To<BSTextRepository>().WhenAnyAncestorNamed("TextRepo");
            Bind<BookStorePersistence.IBSRepository>().To<BSJsonRepository>().WhenAnyAncestorNamed("JsonRepo");
        }
    }
}
