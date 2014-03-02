using System;
using System.Reactive.Disposables;
using Bebbs.Harmonize.With.Harmony.Messages;

namespace Bebbs.Harmonize.With.Harmony.State
{
    public class Started : StoppableState, IState<IRegistrationContext>
    {
        private readonly Messages.IMediator _messageMediator;
        private readonly IAsyncHelper _asyncHelper;
        private readonly Messages.IFactory _messageFactory;

        private IDisposable _subscription;

        public Started(Messages.IMediator messageMediator, IAsyncHelper asyncHelper, Messages.IFactory messageFactory) : base(messageMediator, asyncHelper)
        {
            _messageMediator = messageMediator;
            _asyncHelper = asyncHelper;
            _messageFactory = messageFactory;
        }

        protected override void Stop(IContext context, IStopHarmonizingMessage message)
        {
            _messageMediator.Publish(new TransitionToStateMessage<IContext>(Name.Deregistration, (IRegistrationContext)context));
        }

        private void ProcessCommand(IActiveContext context, With.Command.ICommand command)
        {
            IHarmonyCommandMessage harmonyCommand = _messageFactory.ConstructHarmonyCommand(context.Session, command);

            _messageMediator.Publish(harmonyCommand);
        }

        public void OnEnter(IRegistrationContext context)
        {
            Instrumentation.State.EnteringState(Name.Started);

            base.EnterStoppable(context);

            _subscription = new CompositeDisposable(
                _messageMediator.GetEvent<With.Command.ICommand>().Subscribe(command => ProcessCommand(context, command))
            );

            _messageMediator.Publish(new Messages.StartedMessage());

            Instrumentation.State.EnteredState(Name.Started);
        }

        public void OnExit(IRegistrationContext context)
        {
            Instrumentation.State.ExitingState(Name.Started);

            if (_subscription != null)
            {
                _subscription.Dispose();
                _subscription = null;
            }

            base.ExitStoppable(context);

            Instrumentation.State.ExitedState(Name.Started);
        }
    }
}
