using EventSourceProxy;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Owl.Intuition
{
    public class Module : NinjectModule
    {
        public override void Load()
        {
            Bind<Bootstrapper>().ToSelf();
            Bind<IBootstrapper>().ToMethod(ctx => TracingProxy.CreateWithActivityScope<IBootstrapper>(ctx.Kernel.Get<Bootstrapper>()));
        }
    }
}
