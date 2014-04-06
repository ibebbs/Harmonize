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
        private StandardKernel _kernel;

        public Harmonizer(IOptions options)
        {
            _options = options;
        }

        private void LoadModules(string filePattern)
        {
            try
            {
                Instrumentation.Harmonization.LoadingModules(filePattern);

                _kernel.Load(filePattern);
            }
            catch (Exception e)
            {
                Instrumentation.Error.LoadingModules(filePattern, e);
            }
        }

        private void CreateKernel()
        {
            _kernel = new StandardKernel();
            _kernel.Load(new Module());
            _kernel.Load(new State.Module());

            _options.ModulePatterns.ForEach(LoadModules);

            _options.Modules.ForEach(module => _kernel.Load(module));
        }

        private void Initialize()
        {
            IEnumerable<With.IInitialize> initializables = _kernel.GetAll<With.IInitialize>();

            foreach (With.IInitialize initializable in initializables)
            {
                try
                {
                    initializable.Initialize();
                }
                catch (Exception e)
                {
                    Instrumentation.Error.Initializing(initializable.GetType().Name, e);
                }
            }
        }

        private async Task StartSafely(With.IStart startable)
        {
            try
            {
                await startable.Start();
            }
            catch (Exception exception)
            {
                Instrumentation.Error.Starting(startable.GetType().Name, exception);
            }
        }

        private async Task StopSafely(With.IStop stoppable)
        {
            try
            {
                await stoppable.Stop();
            }
            catch (Exception exception)
            {
                Instrumentation.Error.Stopping(stoppable.GetType().Name, exception);
            }
        }

        private async Task StartHarmonizing()
        {
            With.IGlobalEventAggregator eventAggregator = _kernel.Get<With.IGlobalEventAggregator>();

            eventAggregator.Publish(new With.Message.Starting());

            Task[] startables = _kernel.GetAll<With.IStart>().Select(StartSafely).ToArray();

            await Task.WhenAll(startables);

            eventAggregator.Publish(new With.Message.Started());
        }

        private async Task StopHarmonizing()
        {
            With.IGlobalEventAggregator eventAggregator = _kernel.Get<With.IGlobalEventAggregator>();

            eventAggregator.Publish(new With.Message.Stopping());

            Task[] stoppables = _kernel.GetAll<With.IStop>().Select(StopSafely).ToArray();

            await Task.WhenAll(stoppables);

            eventAggregator.Publish(new With.Message.Stopped());
        }

        private void Cleanup()
        {
            IEnumerable<With.ICleanup> cleanupables = _kernel.GetAll<With.ICleanup>();

            foreach (With.ICleanup cleanupable in cleanupables)
            {
                cleanupable.Cleanup();
            }
        }

        public Task Start()
        {
            Instrumentation.Harmonization.Starting(_options);

            CreateKernel();

            Initialize();

            return StartHarmonizing();
        }

        public async Task Stop()
        {
            Instrumentation.Harmonization.Stopping();

            await StopHarmonizing();

            Cleanup();
        }
    }
}
