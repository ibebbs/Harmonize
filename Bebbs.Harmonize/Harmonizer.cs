using Bebbs.Harmonize.With;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bebbs.Harmonize
{
    public class Harmonizer
    {
        private readonly IOptions _options;

        private IKernel _kernel;
        private IService _service;

        public Harmonizer(IOptions options)
        {
            _options = options;
        }

        private void LoadModules(IModulePattern modulePattern)
        {
            string pattern = Path.Combine(modulePattern.Path, modulePattern.Pattern);

            try
            {
                Instrumentation.Harmonization.LoadingModules(pattern);

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

            _options.ModulePatterns.ForEach(LoadModules);

            _options.Modules.ForEach(module => _kernel.Load(module));
        }

        private void DisposeKernel()
        {
            if (_kernel != null)
            {
                _kernel.Dispose();
                _kernel = null;
            }
        }

        public void Start()
        {
            Instrumentation.Harmonization.Starting(_options);

            CreateKernel();

            _service = _kernel.Get<IService>();
            _service.Initialize();
            _service.Start();
        }

        public void Stop()
        {
            Instrumentation.Harmonization.Stopping();

            if (_service != null)
            {
                _service.Stop();
                _service.Cleanup();
                _service = null;
            }

            DisposeKernel();
        }
    }
}
