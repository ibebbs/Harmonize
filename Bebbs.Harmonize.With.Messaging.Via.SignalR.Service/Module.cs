using Microsoft.AspNet.SignalR.Hubs;
using Ninject.Modules;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Service
{
    public class Module : NinjectModule
    {
        public override void Load()
        {
            Bind<IHubActivator>().To<HubActivator>().InSingletonScope();

            Bind<IMapper>().To<Mapper>().InSingletonScope();

            Bind<Registration.IFactory>().To<Registration.Factory>().InSingletonScope();
            Bind<IHarmonizeConnector>().To<HarmonizeConnector>().InSingletonScope();
            Bind<HarmonizeHub>().ToSelf();
        }
    }
}
