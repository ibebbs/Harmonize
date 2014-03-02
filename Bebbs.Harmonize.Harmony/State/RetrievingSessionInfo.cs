using System;
using System.Reactive.Linq;
using Bebbs.Harmonize.With.Harmony.Messages;

namespace Bebbs.Harmonize.With.Harmony.State
{
    internal class RetrievingSessionInfo : StoppableState, IState<IAuthenticatedContext>
    {
        private readonly Settings.IProvider _settingsProvider;
        private readonly Hub.Authentication.IProvider _authenticationProvider;
        private readonly Messages.IMediator _messageMediator;
        private readonly IAsyncHelper _asyncHelper;

        private IDisposable _subscription;

        public RetrievingSessionInfo(Settings.IProvider settingsProvider, Hub.Authentication.IProvider authenticationProvider, Messages.IMediator messageMediator, IAsyncHelper asyncHelper) : base(messageMediator, asyncHelper)
        {
            _settingsProvider = settingsProvider;
            _authenticationProvider = authenticationProvider;
            _messageMediator = messageMediator;
            _asyncHelper = asyncHelper;
        }

        protected override void Stop(IContext context, IStopHarmonizingMessage message)
        {
            _messageMediator.Publish(new TransitionToStateMessage<IContext>(Name.Stopping, context));
        }

        private async void Authenticate(IAuthenticatedContext context)
        {
            Settings.IValues settings = _settingsProvider.GetValues();

            try
            {
                Hub.Session.IInfo sessionInfo = await _authenticationProvider.AuthenticateAsync(settings.HarmonyHubAddress, context.AuthenticationToken);

                ISessionContext sessionContext = context.ForSession(sessionInfo);

                _messageMediator.Publish(new TransitionToStateMessage<ISessionContext>(Name.EstablishingSession, sessionContext));
            }
            catch
            {
                _messageMediator.Publish(new TransitionToStateMessage<IContext>(Name.Stopped, context));
            }
        }

        public void OnEnter(IAuthenticatedContext context)
        {
            Instrumentation.State.EnteringState(Name.RetrievingSessionInfo);

            base.EnterStoppable(context);

            Authenticate(context);

            Instrumentation.State.EnteredState(Name.RetrievingSessionInfo);
        }

        public void OnExit(IAuthenticatedContext context)
        {
            Instrumentation.State.ExitingState(Name.RetrievingSessionInfo);

            if (_subscription != null)
            {
                _subscription.Dispose();
                _subscription = null;
            }

            base.ExitStoppable(context);

            Instrumentation.State.ExitedState(Name.RetrievingSessionInfo);
        }
    }
}
