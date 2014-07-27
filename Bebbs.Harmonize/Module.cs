using EventSourceProxy;
using Ninject;
using Ninject.Modules;

namespace Bebbs.Harmonize
{
    public class Module : NinjectModule
    {
        public override void Load()
        {
            Bind<Harmonizer>().ToSelf();

            Bind<IHarmonizer>().ToMethod(ctx => TracingProxy.CreateWithActivityScope<IHarmonizer>(ctx.Kernel.Get<Harmonizer>()));
        }
    }
}
