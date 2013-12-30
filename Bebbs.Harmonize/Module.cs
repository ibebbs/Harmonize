using Bebbs.Harmonize.With;
using Ninject.Modules;

namespace Bebbs.Harmonize
{
    public class Module : NinjectModule
    {
        public override void Load()
        {
            Bind<IGlobalEventAggregator>().To<GlobalEventAggregator>().InSingletonScope();
            Bind<IAsyncHelper>().To<AsyncHelper>().InSingletonScope();
        }
    }
}
