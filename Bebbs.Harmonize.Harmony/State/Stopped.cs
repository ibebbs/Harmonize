using Bebbs.Harmonize.With;
using Bebbs.Harmonize.Harmony.Messages;
using System;

namespace Bebbs.Harmonize.Harmony.State
{
    public class Stopped : IState<IContext>
    {
        private readonly IGlobalEventAggregator _eventAggregator;
        private readonly IAsyncHelper _asyncHelper;

        private IDisposable _subscription;

        public Stopped(IGlobalEventAggregator eventAggregator, IAsyncHelper asyncHelper)
        {
            _eventAggregator = eventAggregator;
            _asyncHelper = asyncHelper;
        }

        private void ProcessMessage(IContext context, IStartHarmonizingMessage message)
        {
            _eventAggregator.Publish(new TransitionToStateMessage<IContext>(Name.Starting, context));
        }

        public void OnEnter(IContext context)
        {
            EventSource.Log.EnteringState(Name.Stopped);

            _subscription = _eventAggregator.GetEvent<IStartHarmonizingMessage>().Subscribe(message => ProcessMessage(context, message));

            _eventAggregator.Publish(new Messages.StoppedMessage());

            EventSource.Log.EnteringState(Name.Stopped);
        }

        public void OnExit(IContext context)
        {
            EventSource.Log.ExitingState(Name.Stopped);

            if (_subscription != null)
            {
                _subscription.Dispose();
                _subscription = null;
            }

            EventSource.Log.ExitedState(Name.Stopped);
        }
    }
}
