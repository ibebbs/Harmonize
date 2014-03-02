using System;
using System.Reactive.Linq;
using System.Security.Authentication;
using Bebbs.Harmonize.With.Harmony.Messages;

namespace Bebbs.Harmonize.With.Harmony.State
{
    internal class Authenticating : StoppableState, IState<IContext>
    {
        private readonly Messages.IMediator _messageMediator;
        private readonly IAsyncHelper _asyncHelper;

        private IDisposable _subscription;

        public Authenticating(Messages.IMediator messageMediator, IAsyncHelper asyncHelper) : base(messageMediator, asyncHelper)
        {
            _messageMediator = messageMediator;
            _asyncHelper = asyncHelper;
        }

        private void ProcessResponse(IContext context, IAuthenticationResponseMessage message)
        {
            if (message.Success)
            {
                IAuthenticatedContext authenticatedContext = context.WithAuthentication(message.AccountId, message.AuthenticationToken);

                _messageMediator.Publish(new TransitionToStateMessage<IAuthenticatedContext>(Name.RetrievingSessionInfo, authenticatedContext));
            }
            else
            {
                IFaultedContext faultedContext = context.Faulted(new AuthenticationException(message.Message));

                _messageMediator.Publish(new TransitionToStateMessage<IFaultedContext>(Name.Faulted, faultedContext));
            }
        }

        private void ProcessException(IContext context, Exception exception)
        {
            _messageMediator.Publish(new TransitionToStateMessage<IContext>(Name.Stopped, context));
        }

        protected override void Stop(IContext context, IStopHarmonizingMessage message)
        {
            _messageMediator.Publish(new TransitionToStateMessage<IContext>(Name.Stopping, context));
        }

        public void OnEnter(IContext context)
        {
            Instrumentation.State.EnteringState(Name.Authenticating);

            base.EnterStoppable(context);

            _subscription = _messageMediator.GetEvent<IAuthenticationResponseMessage>().Timeout(TimeSpan.FromSeconds(30)).Subscribe(response => ProcessResponse(context, response), exception => ProcessException(context, exception));

            _messageMediator.Publish(new RequestAuthenticationMessage(context.EMail, context.Password));

            Instrumentation.State.EnteredState(Name.Authenticating);
        }

        public void OnExit(IContext context)
        {
            Instrumentation.State.ExitingState(Name.Authenticating);

            if (_subscription != null)
            {
                _subscription.Dispose();
                _subscription = null;
            }

            base.ExitStoppable(context);

            Instrumentation.State.ExitedState(Name.Authenticating);
        }
    }
}
