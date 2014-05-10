using Ninject.Modules;

namespace Bebbs.Harmonize.With
{
    public class Module : NinjectModule
    {
        public override void Load()
        {
            Bind<Message.IFactory>().To<Message.Factory>().InSingletonScope();
        }
    }
}
