using System;
using System.Reactive.Linq;
using Bebbs.Harmonize.With.Harmony.Messages;

namespace Bebbs.Harmonize.With.Harmony.State
{
    internal class EstablishingSession : StoppableState, IState<ISessionContext>
    {
        private readonly IGlobalEventAggregator _eventAggregator;
        private readonly IAsyncHelper _asyncHelper;

        private IDisposable _subscription;

        public EstablishingSession(IGlobalEventAggregator eventAggregator, IAsyncHelper asyncHelper) : base(eventAggregator, asyncHelper)
        {
            _eventAggregator = eventAggregator;
            _asyncHelper = asyncHelper;
        }

        private void ProcessResponse(ISessionContext context, ISessionResponseMessage response)
        {
            IActiveContext activeContext = context.Activate(response.SessionInfo, response.Session);

            _eventAggregator.Publish(new TransitionToStateMessage<IActiveContext>(Name.Synchronizing, activeContext));
        }

        private void ProcessException(ISessionContext context, Exception exception)
        {
            _eventAggregator.Publish(new TransitionToStateMessage<IContext>(Name.Stopped, context));
        }

        protected override void Stop(IContext context, IStopHarmonizingMessage message)
        {
            _eventAggregator.Publish(new TransitionToStateMessage<IContext>(Name.Stopping, context));
        }

        public void OnEnter(ISessionContext context)
        {
            EventSource.Log.EnteringState(Name.EstablishingSession);

            base.EnterStoppable(context);

            _subscription = _eventAggregator.GetEvent<ISessionResponseMessage>().Timeout(TimeSpan.FromSeconds(30)).Subscribe(response => ProcessResponse(context, response), exception => ProcessException(context, exception));

            _eventAggregator.Publish(new RequestSessionMessage(context.SessionInfo));

            EventSource.Log.EnteredState(Name.EstablishingSession);
        }

        public void OnExit(ISessionContext context)
        {
            EventSource.Log.ExitingState(Name.EstablishingSession);

            if (_subscription != null)
            {
                _subscription.Dispose();
                _subscription = null;
            }

            base.ExitStoppable(context);

            EventSource.Log.ExitedState(Name.EstablishingSession);
        }
    }
}
