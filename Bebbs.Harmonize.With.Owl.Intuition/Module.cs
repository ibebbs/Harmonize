
namespace Bebbs.Harmonize.With.Owl.Intuition
{
    public class Module : HarmonizedModule
    {
        public override void Load()
        {
            Kernel.Bind<Configuration.IProvider>().To<Configuration.Provider>().InSingletonScope();
            Kernel.Bind<Device.IFactory>().To<Device.Factory>().InSingletonScope();

            Kernel.Bind<IInitialize, ICleanup, IStart, IStop>().To<Connector>().InSingletonScope();
        }
    }
}
