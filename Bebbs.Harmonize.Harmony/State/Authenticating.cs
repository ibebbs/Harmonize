using System;
using System.Reactive.Linq;
using System.Security.Authentication;
using Bebbs.Harmonize.With.Harmony.Messages;

namespace Bebbs.Harmonize.With.Harmony.State
{
    internal class Authenticating : StoppableState, IState<IContext>
    {
        private readonly IGlobalEventAggregator _eventAggregator;
        private readonly IAsyncHelper _asyncHelper;

        private IDisposable _subscription;

        public Authenticating(IGlobalEventAggregator eventAggregator, IAsyncHelper asyncHelper) : base(eventAggregator, asyncHelper)
        {
            _eventAggregator = eventAggregator;
            _asyncHelper = asyncHelper;
        }

        private void ProcessResponse(IContext context, IAuthenticationResponseMessage message)
        {
            if (message.Success)
            {
                IAuthenticatedContext authenticatedContext = context.WithAuthentication(message.AccountId, message.AuthenticationToken);

                _eventAggregator.Publish(new TransitionToStateMessage<IAuthenticatedContext>(Name.RetrievingSessionInfo, authenticatedContext));
            }
            else
            {
                IFaultedContext faultedContext = context.Faulted(new AuthenticationException(message.Message));

                _eventAggregator.Publish(new TransitionToStateMessage<IFaultedContext>(Name.Faulted, faultedContext));
            }
        }

        private void ProcessException(IContext context, Exception exception)
        {
            _eventAggregator.Publish(new TransitionToStateMessage<IContext>(Name.Stopped, context));
        }

        protected override void Stop(IContext context, IStopHarmonizingMessage message)
        {
            _eventAggregator.Publish(new TransitionToStateMessage<IContext>(Name.Stopping, context));
        }

        public void OnEnter(IContext context)
        {
            EventSource.Log.EnteringState(Name.Authenticating);

            base.EnterStoppable(context);

            _subscription = _eventAggregator.GetEvent<IAuthenticationResponseMessage>().Timeout(TimeSpan.FromSeconds(30)).Subscribe(response => ProcessResponse(context, response), exception => ProcessException(context, exception));

            _eventAggregator.Publish(new RequestAuthenticationMessage(context.EMail, context.Password));

            EventSource.Log.EnteredState(Name.Authenticating);
        }

        public void OnExit(IContext context)
        {
            EventSource.Log.ExitingState(Name.Authenticating);

            if (_subscription != null)
            {
                _subscription.Dispose();
                _subscription = null;
            }

            base.ExitStoppable(context);

            EventSource.Log.ExitedState(Name.Authenticating);
        }
    }
}
