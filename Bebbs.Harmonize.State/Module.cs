using Bebbs.Harmonize.With;
using Ninject.Modules;

namespace Bebbs.Harmonize.State
{
    public class Module : NinjectModule
    {
        public override void Load()
        {
            Bind<Event.ITranslator>().To<Event.Translator>().InSingletonScope();

            Bind<IStore, IInitializeAtStartup, ICleanupAtShutdown>().To<Store>().InSingletonScope();
        }
    }
}
