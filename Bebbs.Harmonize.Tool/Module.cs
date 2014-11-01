using Caliburn.Micro;
using Ninject.Modules;

namespace Bebbs.Harmonize.Tool
{
    public class Module : NinjectModule
    {
        public override void Load()
        {
            Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();

            Bind<Service.IBridge>().To<Service.Bridge>().InSingletonScope();

            Bind<IMainWindowViewModel>().To<MainWindowViewModel>();
        }
    }
}
