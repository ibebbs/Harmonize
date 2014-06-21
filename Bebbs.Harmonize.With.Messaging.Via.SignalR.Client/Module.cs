using Ninject;
using Ninject.Modules;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Client
{
    public class Module : NinjectModule
    {
        public override void Load()
        {
            Bind<Configuration.IProvider>().To<Configuration.Provider>().InSingletonScope();
            Bind<Configuration.ISettings>().ToMethod(ctx => ctx.Kernel.Get<Configuration.IProvider>().GetSettings()).InSingletonScope();

            Bind<IMapper>().To<Mapper>().InSingletonScope();

            Bind<Hub.IFactory>().To<Hub.Factory>().InSingletonScope();
            Bind<IEndpoint>().To<Endpoint>();
        }
    }
}
