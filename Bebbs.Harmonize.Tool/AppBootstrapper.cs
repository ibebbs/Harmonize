using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Ninject;

namespace Bebbs.Harmonize.Tool
{
    public class AppBootstrapper : BootstrapperBase
    {
        private IKernel _kernel;

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _kernel = new StandardKernel(new Module());
        }

        protected override object GetInstance(Type service, string key)
        {
            var instance = _kernel.Get(service, key);
            if (instance != null)
                return instance;

            throw new InvalidOperationException("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _kernel.GetAll(service);
        }

        protected override void BuildUp(object instance)
        {
            _kernel.Inject(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            DisplayRootViewFor<IMainWindowViewModel>();
        }
    }
}