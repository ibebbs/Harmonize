using Bebbs.Harmonize.With;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.Host
{
    public class Container<TService> where TService : IService
    {
        private readonly Configuration.IProvider _configurationProvider;
        private readonly IEnumerable<INinjectModule> _modules;

        private IKernel _kernel;
        private TService _service;

        public Container(Configuration.IProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
            _modules = Enumerable.Empty<INinjectModule>();
        }

        public Container(IEnumerable<INinjectModule> modules) : this(new Configuration.Provider())
        {
            _modules = (modules ?? Enumerable.Empty<INinjectModule>()).ToArray();
        }

        public Container(params INinjectModule[] modules) : this(new Configuration.Provider()) 
        {
            _modules = modules;
        }

        private void LoadModules(Configuration.IModulePattern modulePattern)
        {
            string pattern = Path.Combine(modulePattern.Path, modulePattern.Pattern);

            try
            {
                Instrumentation.Container.LoadingModules(pattern);

                _kernel.Load(pattern);
            }
            catch (Exception e)
            {
                Instrumentation.Error.LoadingModules(pattern, e);
            }
        }

        private void CreateKernel()
        {
            _kernel = new StandardKernel();
            _kernel.Load(new Module());
            _kernel.Load(new With.Module());

            Configuration.ISettings settings = _configurationProvider.GetSettings();

            settings.ModulePatterns.ForEach(LoadModules);

            _modules.ForEach(module => _kernel.Load(module));
        }

        private void DisposeKernel()
        {
            if (_kernel != null)
            {
                _kernel.Dispose();
                _kernel = null;
            }
        }

        public async Task Start()
        {
            Instrumentation.Container.Starting();

            CreateKernel();

            _service = _kernel.Get<TService>();
            _service.Initialize();

            await _service.Start();
        }

        public async Task Stop()
        {
            Instrumentation.Container.Stopping();

            if (_service != null)
            {
                await _service.Stop();

                _service.Cleanup();
                _service = default(TService);
            }

            DisposeKernel();
        }
    }
}
