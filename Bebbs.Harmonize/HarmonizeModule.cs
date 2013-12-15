using Bebbs.Harmonize.Common;
using Ninject.Modules;

namespace Bebbs.Harmonize
{
    public class HarmonizeModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IGlobalEventAggregator>().To<GlobalEventAggregator>().InSingletonScope();
            Bind<IAsyncHelper>().To<AsyncHelper>().InSingletonScope();
        }
    }
}
