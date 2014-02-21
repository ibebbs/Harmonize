using System;
using System.Reactive.Linq;
using Bebbs.Harmonize.With.Harmony.Messages;

namespace Bebbs.Harmonize.With.Harmony.State
{
    internal class RetrievingSessionInfo : StoppableState, IState<IAuthenticatedContext>
    {
        private readonly IGlobalEventAggregator _eventAggregator;
        private readonly IAsyncHelper _asyncHelper;

        private IDisposable _subscription;

        public RetrievingSessionInfo(IGlobalEventAggregator eventAggregator, IAsyncHelper asyncHelper) : base(eventAggregator, asyncHelper)
        {
            _eventAggregator = eventAggregator;
            _asyncHelper = asyncHelper;
        }

        private void ProcessResponse(IAuthenticatedContext context, ISessionInfoResponseMessage response)
        {
            ISessionContext sessionContext = context.ForSession(response.SessionInfo);

            _eventAggregator.Publish(new TransitionToStateMessage<ISessionContext>(Name.EstablishingSession, sessionContext));
        }

        private void ProcessException(IAuthenticatedContext context, Exception exception)
        {
            _eventAggregator.Publish(new TransitionToStateMessage<IContext>(Name.Stopped, context));
        }

        protected override void Stop(IContext context, IStopHarmonizingMessage message)
        {
            _eventAggregator.Publish(new TransitionToStateMessage<IContext>(Name.Stopping, context));
        }

        public void OnEnter(IAuthenticatedContext context)
        {
            EventSource.Log.EnteringState(Name.RetrievingSessionInfo);

            base.EnterStoppable(context);

            _subscription = _eventAggregator.GetEvent<ISessionInfoResponseMessage>().Timeout(TimeSpan.FromSeconds(30)).Subscribe(response => ProcessResponse(context, response), exception => ProcessException(context, exception));

            _eventAggregator.Publish(new RequestSessionInfoMessage(context.AuthenticationToken));

            EventSource.Log.EnteredState(Name.RetrievingSessionInfo);
        }

        public void OnExit(IAuthenticatedContext context)
        {
            EventSource.Log.ExitingState(Name.RetrievingSessionInfo);

            if (_subscription != null)
            {
                _subscription.Dispose();
                _subscription = null;
            }

            base.ExitStoppable(context);

            EventSource.Log.ExitedState(Name.RetrievingSessionInfo);
        }
    }
}
