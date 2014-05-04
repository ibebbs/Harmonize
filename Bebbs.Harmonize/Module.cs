using Bebbs.Harmonize.With;
using Ninject.Modules;

namespace Bebbs.Harmonize
{
    public class Module : NinjectModule
    {
        public override void Load()
        {
            Bind<IClock>().To<Clock>().InSingletonScope();
            Bind<IGlobalEventAggregator>().To<GlobalEventAggregator>().InSingletonScope();
            Bind<IAsyncHelper>().To<AsyncHelper>().InSingletonScope();

            Bind<IService>().To<Service>().InSingletonScope();
        }
    }
}
