using BookStorePersistence;
using Ninject.Modules;

namespace BookStoreService
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBSService>().To<BSService>();
            Bind<IBSRepository>().To<BSTextRepository>().Named("TextRepo");
            Bind<IBSRepository>().To<BSJsonRepository>().Named("JsonRepo");
        }
    }
}
