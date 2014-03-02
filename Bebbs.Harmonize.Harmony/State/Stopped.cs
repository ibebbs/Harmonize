using System;
using Bebbs.Harmonize.With.Harmony.Messages;

namespace Bebbs.Harmonize.With.Harmony.State
{
    public class Stopped : IState<IContext>
    {
        private readonly Messages.IMediator _messageMediator;
        private readonly IAsyncHelper _asyncHelper;

        private IDisposable _subscription;

        public Stopped(Messages.IMediator messageMediator, IAsyncHelper asyncHelper)
        {
            _messageMediator = messageMediator;
            _asyncHelper = asyncHelper;
        }

        private void ProcessMessage(IContext context, IStartHarmonizingMessage message)
        {
            _messageMediator.Publish(new TransitionToStateMessage<IContext>(Name.Starting, context));
        }

        public void OnEnter(IContext context)
        {
            Instrumentation.State.EnteringState(Name.Stopped);

            _subscription = _messageMediator.GetEvent<IStartHarmonizingMessage>().Subscribe(message => ProcessMessage(context, message));

            _messageMediator.Publish(new Messages.StoppedMessage());

            Instrumentation.State.EnteringState(Name.Stopped);
        }

        public void OnExit(IContext context)
        {
            Instrumentation.State.ExitingState(Name.Stopped);

            if (_subscription != null)
            {
                _subscription.Dispose();
                _subscription = null;
            }

            Instrumentation.State.ExitedState(Name.Stopped);
        }
    }
}
