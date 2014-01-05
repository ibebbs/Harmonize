using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Alljoyn
{
    public class Module : NinjectModule
    {
        public override void Load()
        {
            Bind<Bus.Attachment.IFactory>().To<Bus.Attachment.Factory>().InSingletonScope();
            Bind<Bus.Object.IFactory>().To<Bus.Object.Factory>().InSingletonScope();

            Bind<Bus.ICoordinator>().To<Bus.Coordinator>();

            Bind<IInitializeAtStartup, ICleanupAtShutdown>().To<Harmonizer>().InSingletonScope();
        }
    }
}
