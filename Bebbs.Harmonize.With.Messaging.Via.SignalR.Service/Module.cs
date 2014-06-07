using Ninject.Modules;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Service
{
    public class Module : NinjectModule
    {
        public override void Load()
        {
            Bind<IMapper>().To<Mapper>().InSingletonScope();

            Bind<Registration.IFactory>().To<Registration.Factory>().InSingletonScope();
        }
    }
}
