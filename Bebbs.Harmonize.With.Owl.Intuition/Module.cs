
namespace Bebbs.Harmonize.With.Owl.Intuition
{
    public class Module : HarmonizedModule
    {
        public override void Load()
        {
            Kernel.Bind<Configuration.IProvider>().To<Configuration.Provider>().InSingletonScope();
            Kernel.Bind<Gateway.IFactory>().To<Gateway.Factory>().InSingletonScope();

            Kernel.Bind<IInitialize, ICleanup, IStart, IStop>().To<Connector>().InSingletonScope();
        }
    }
}
