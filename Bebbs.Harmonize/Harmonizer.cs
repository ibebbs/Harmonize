using Bebbs.Harmonize.Common;
using Bebbs.Harmonize.Harmony;
using Bebbs.Harmonize.Harmony.Command;
using Bebbs.Harmonize.Harmony.Messages;
using Ninject;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Bebbs.Harmonize
{
    public class Harmonizer
    {
        private readonly StandardKernel _kernel;

        public Harmonizer()
        {
            _kernel = new StandardKernel();
            _kernel.Load(new HarmonizeModule());
            _kernel.Load(new HarmonyModule());
        }

        private void SetValues(Common.Settings.IProvider settingsProvider)
        {
            _kernel.Bind<Common.Settings.IProvider>().ToConstant(settingsProvider);
        }

        private void Initialize()
        {
            IEnumerable<IInitializeAtStartup> initializables = _kernel.GetAll<IInitializeAtStartup>();

            foreach (IInitializeAtStartup initializable in initializables)
            {
                initializable.Initialize();
            }
        }

        private Task StartHarmonizing()
        {
            IGlobalEventAggregator eventAggregator = _kernel.Get<IGlobalEventAggregator>();

            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

            eventAggregator.GetEvent<IStartedMessage>().Take(1).Subscribe(message => tcs.SetResult(null));
            eventAggregator.GetEvent<IErrorMessage>().Take(1).Subscribe(message => tcs.SetException(message.Exception));

            eventAggregator.Publish(new StartHarmonizingMessage());

            return tcs.Task;
        }

        private Task StopHarmonizing()
        {
            IGlobalEventAggregator eventAggregator = _kernel.Get<IGlobalEventAggregator>();

            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            eventAggregator.GetEvent<IStoppedMessage>().Take(1).Subscribe(message => tcs.SetResult(null));
            eventAggregator.GetEvent<IErrorMessage>().Take(1).Subscribe(message => tcs.SetException(message.Exception));

            eventAggregator.Publish(new StopHarmonizingMessage());

            return tcs.Task;
        }

        private void Cleanup()
        {
            IEnumerable<ICleanupAtShutdown> cleanupables = _kernel.GetAll<ICleanupAtShutdown>();

            foreach (ICleanupAtShutdown cleanupable in cleanupables)
            {
                cleanupable.Cleanup();
            }
        }

        public Task Start(Common.Settings.IProvider settingsProvider)
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

        public void SendCommand(ICommand command)
        {
            IGlobalEventAggregator eventAggregator = _kernel.Get<IGlobalEventAggregator>();

            eventAggregator.Publish(command);
        }
    }
}
