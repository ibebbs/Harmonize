using Bebbs.Harmonize.Harmony;
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
            _kernel.Load(new Module());
            _kernel.Load(new Harmony.Module());
        }

        private void SetValues(With.Settings.IProvider settingsProvider)
        {
            _kernel.Bind<With.Settings.IProvider>().ToConstant(settingsProvider);
        }

        private void Initialize()
        {
            IEnumerable<With.IInitializeAtStartup> initializables = _kernel.GetAll<With.IInitializeAtStartup>();

            foreach (With.IInitializeAtStartup initializable in initializables)
            {
                initializable.Initialize();
            }
        }

        private Task<Harmony.Hub.Configuration.IValues> StartHarmonizing()
        {
            With.IGlobalEventAggregator eventAggregator = _kernel.Get<With.IGlobalEventAggregator>();

            TaskCompletionSource<Harmony.Hub.Configuration.IValues> tcs = new TaskCompletionSource<Harmony.Hub.Configuration.IValues>();

            eventAggregator.GetEvent<IStartedMessage>().Take(1).Subscribe(message => tcs.SetResult(message.HarmonyConfiguration));
            eventAggregator.GetEvent<IErrorMessage>().Take(1).Subscribe(message => tcs.SetException(message.Exception));

            eventAggregator.Publish(new StartHarmonizingMessage());

            return tcs.Task;
        }

        private Task StopHarmonizing()
        {
            With.IGlobalEventAggregator eventAggregator = _kernel.Get<With.IGlobalEventAggregator>();

            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            eventAggregator.GetEvent<IStoppedMessage>().Take(1).Subscribe(message => tcs.SetResult(null));
            eventAggregator.GetEvent<IErrorMessage>().Take(1).Subscribe(message => tcs.SetException(message.Exception));

            eventAggregator.Publish(new StopHarmonizingMessage());

            return tcs.Task;
        }

        private void Cleanup()
        {
            IEnumerable<With.ICleanupAtShutdown> cleanupables = _kernel.GetAll<With.ICleanupAtShutdown>();

            foreach (With.ICleanupAtShutdown cleanupable in cleanupables)
            {
                cleanupable.Cleanup();
            }
        }

        public Task<Harmony.Hub.Configuration.IValues> Start(With.Settings.IProvider settingsProvider)
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

        public void SendCommand(With.Command.ICommand command)
        {
            With.IGlobalEventAggregator eventAggregator = _kernel.Get<With.IGlobalEventAggregator>();

            eventAggregator.Publish(command);
        }
    }
}
