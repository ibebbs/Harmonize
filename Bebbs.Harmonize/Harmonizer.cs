﻿using Bebbs.Harmonize.Harmony.Messages;
using Bebbs.Harmonize.With;
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
            IEnumerable<With.IInitializeAtStartup> initializables = _kernel.GetAll<With.IInitializeAtStartup>();

            foreach (With.IInitializeAtStartup initializable in initializables)
            {
                initializable.Initialize();
            }
        }

        private Task StartHarmonizing()
        {
            With.IGlobalEventAggregator eventAggregator = _kernel.Get<With.IGlobalEventAggregator>();

            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

            eventAggregator.GetEvent<With.Message.IStarted>().Take(1).Subscribe(message => tcs.SetResult(null));
            eventAggregator.GetEvent<IErrorMessage>().Take(1).Subscribe(message => tcs.SetException(message.Exception));

            eventAggregator.Publish(new StartHarmonizingMessage());

            return tcs.Task;
        }

        private Task StopHarmonizing()
        {
            With.IGlobalEventAggregator eventAggregator = _kernel.Get<With.IGlobalEventAggregator>();

            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            eventAggregator.GetEvent<With.Message.IStopped>().Take(1).Subscribe(message => tcs.SetResult(null));
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

        public void SendCommand(With.Command.ICommand command)
        {
            With.IGlobalEventAggregator eventAggregator = _kernel.Get<With.IGlobalEventAggregator>();

            eventAggregator.Publish(command);
        }
    }
}
