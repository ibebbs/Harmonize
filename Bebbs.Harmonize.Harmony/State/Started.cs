using Bebbs.Harmonize.Common;
using Bebbs.Harmonize.Harmony.Command;
using Bebbs.Harmonize.Harmony.Messages;
using System;
using System.Reactive.Disposables;

namespace Bebbs.Harmonize.Harmony.State
{
    public class Started : StoppableState, IState<IStartedContext>
    {
        private readonly IGlobalEventAggregator _eventAggregator;
        private readonly IAsyncHelper _asyncHelper;
        private readonly Command.IFactory _harmonyCommandFactory;

        private IDisposable _subscription;

        public Started(IGlobalEventAggregator eventAggregator, IAsyncHelper asyncHelper, Command.IFactory harmonyCommandFactory) : base(eventAggregator, asyncHelper)
        {
            _eventAggregator = eventAggregator;
            _asyncHelper = asyncHelper;
            _harmonyCommandFactory = harmonyCommandFactory;
        }

        protected override void Stop(IContext context, IStopHarmonizingMessage message)
        {
            _eventAggregator.Publish(new TransitionToStateMessage<IContext>(Name.Stopping, context));
        }

        private void ProcessCommand(IActiveContext context, ICommand command)
        {
            IHarmonyCommandMessage harmonyCommand = _harmonyCommandFactory.ConstructHarmonyCommand(context.Session, command);

            _eventAggregator.Publish(harmonyCommand);
        }

        public void OnEnter(IStartedContext context)
        {
            EventSource.Log.EnteringState(Name.Started);

            base.EnterStoppable(context);

            _subscription = new CompositeDisposable(
                _eventAggregator.GetEvent<ICommand>().Subscribe(command => ProcessCommand(context, command))
            );

            _eventAggregator.Publish(new StartedMessage(context.HarmonyConfiguration));

            EventSource.Log.EnteredState(Name.Started);
        }

        public void OnExit(IStartedContext context)
        {
            EventSource.Log.ExitingState(Name.Started);

            if (_subscription != null)
            {
                _subscription.Dispose();
                _subscription = null;
            }

            base.ExitStoppable(context);

            EventSource.Log.ExitedState(Name.Started);
        }
    }
}
