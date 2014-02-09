using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Alljoyn
{
    public class Module : HarmonizedModule
    {
        public override void Load()
        {
            Bind<Bus.Attachment.IDescription>().ToConstant(new Bus.Attachment.Description("Harmonize", "unix:abstract=alljoyn", "uk.co.bebbs.harmonize", 33)).InSingletonScope();

            Bind<Bus.Attachment.IFactory>().To<Bus.Attachment.Factory>().InSingletonScope();
            Bind<Bus.Object.IFactory>().To<Bus.Object.Factory>().InSingletonScope();

            Bind<Bus.Coordinator>().ToSelf();
            Bind<Bus.ICoordinator>().ToMethod(ctx => EventSourceProxy.TracingProxy.Create<Bus.ICoordinator>(ctx.Kernel.Get<Bus.Coordinator>())).InSingletonScope();

            Bind<IInitialize, ICleanup>().To<Harmonizer>().InSingletonScope();
        }
    }
}
