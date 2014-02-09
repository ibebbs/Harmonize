using Bebbs.Harmonize.With;
using Bebbs.Harmonize.Harmony.Messages;
using System;
using System.Reactive.Disposables;

namespace Bebbs.Harmonize.Harmony.State
{
    public class Started : StoppableState, IState<IRegistrationContext>
    {
        private readonly IGlobalEventAggregator _eventAggregator;
        private readonly IAsyncHelper _asyncHelper;
        private readonly Messages.IFactory _messageFactory;

        private IDisposable _subscription;

        public Started(IGlobalEventAggregator eventAggregator, IAsyncHelper asyncHelper, Messages.IFactory messageFactory) : base(eventAggregator, asyncHelper)
        {
            _eventAggregator = eventAggregator;
            _asyncHelper = asyncHelper;
            _messageFactory = messageFactory;
        }

        protected override void Stop(IContext context, IStopHarmonizingMessage message)
        {
            _eventAggregator.Publish(new TransitionToStateMessage<IContext>(Name.Deregistration, (IRegistrationContext)context));
        }

        private void ProcessCommand(IActiveContext context, With.Command.ICommand command)
        {
            IHarmonyCommandMessage harmonyCommand = _messageFactory.ConstructHarmonyCommand(context.Session, command);

            _eventAggregator.Publish(harmonyCommand);
        }

        public void OnEnter(IRegistrationContext context)
        {
            EventSource.Log.EnteringState(Name.Started);

            base.EnterStoppable(context);

            _subscription = new CompositeDisposable(
                _eventAggregator.GetEvent<With.Command.ICommand>().Subscribe(command => ProcessCommand(context, command))
            );

            _eventAggregator.Publish(new Messages.StartedMessage());

            EventSource.Log.EnteredState(Name.Started);
        }

        public void OnExit(IRegistrationContext context)
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
