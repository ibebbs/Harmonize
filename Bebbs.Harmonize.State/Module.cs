using Bebbs.Harmonize.With;
using Ninject.Modules;

namespace Bebbs.Harmonize.State
{
    public class Module : NinjectModule
    {
        public override void Load()
        {
            Bind<Event.IMapper>().To<Event.Mapper>().InSingletonScope();
            Bind<Event.ISerializer>().To<Event.Serializer>().InSingletonScope();
            Bind<Event.IEncoder>().To<Event.Encoder>().InSingletonScope();
            Bind<Event.ITranslator>().To<Event.Translator>().InSingletonScope();

            Bind(new [] { typeof(IStore), typeof(IInitialize), typeof(IStart), typeof(IStop), typeof(ICleanup) }).To<Store>().InSingletonScope();
        }
    }
}
