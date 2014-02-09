using Bebbs.Harmonize.With;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bebbs.Harmonize
{
    public class Harmonizer
    {
        private readonly StandardKernel _kernel;

        public Harmonizer(IOptions options)
        {
            _kernel = new StandardKernel();
            _kernel.Load(new Module());
            _kernel.Load(new State.Module());

            options.Modules.ForEach(module => _kernel.Load(module));
        }

        private void SetValues(With.Settings.IProvider settingsProvider)
        {
            _kernel.Bind<With.Settings.IProvider>().ToConstant(settingsProvider);
        }

        private void Initialize()
        {
            IEnumerable<With.IInitialize> initializables = _kernel.GetAll<With.IInitialize>();

            foreach (With.IInitialize initializable in initializables)
            {
                initializable.Initialize();
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
                Instrumentation.Error.WhenStarting(startable, exception);
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
                Instrumentation.Error.WhenStopping(stoppable, exception);
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

        public Task Start(With.Settings.IProvider settingsProvider)
        {
            SetValues(settingsProvider);

            Initialize();

            return StartHarmonizing();
        }

        public async Task Stop()
        {
            await StopHarmonizing();

            Cleanup();
        }
    }
}
