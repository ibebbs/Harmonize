using Ninject;
using Ninject.Modules;

namespace Bebbs.Harmonize.With.Store.Using.EventStore
{
    public class Module : NinjectModule
    {
        public override void Load()
        {
            Bind<Configuration.IProvider>().To<Configuration.Provider>().InSingletonScope();
            Bind<Configuration.ISettings>().ToMethod(ctx => ctx.Kernel.Get<Configuration.IProvider>().GetSettings()).InSingletonScope();

            Bind<Event.IProvider>().To<Event.Provider>().InSingletonScope();
        }
    }
}
