using System;
using System.Reactive.Linq;
using Bebbs.Harmonize.With.Harmony.Messages;

namespace Bebbs.Harmonize.With.Harmony.State
{
    internal class EstablishingSession : StoppableState, IState<ISessionContext>
    {
        private readonly Hub.Endpoint.IFactory _endpointFactory;
        private readonly Settings.IProvider _settingsProvider;
        private readonly Messages.IMediator _messageMediator;
        private readonly IAsyncHelper _asyncHelper;

        public EstablishingSession(Settings.IProvider settingsProvider, Hub.Endpoint.IFactory endpointFactory, Messages.IMediator messageMediator, IAsyncHelper asyncHelper)
            : base(messageMediator, asyncHelper)
        {
            _settingsProvider = settingsProvider;
            _endpointFactory = endpointFactory;
            _messageMediator = messageMediator;
            _asyncHelper = asyncHelper;
        }

        protected override void Stop(IContext context, IStopHarmonizingMessage message)
        {
            _messageMediator.Publish(new TransitionToStateMessage<IContext>(Name.Stopping, context));
        }

        public void OnEnter(ISessionContext context)
        {
            Instrumentation.State.EnteringState(Name.EstablishingSession);

            base.EnterStoppable(context);

            CreateSession(context);

            Instrumentation.State.EnteredState(Name.EstablishingSession);
        }

        private async void CreateSession(ISessionContext context)
        {
            Settings.IValues settings = _settingsProvider.GetValues();

            Hub.Endpoint.IInstance endpoint = _endpointFactory.Create(settings.HarmonyHubAddress, context.SessionInfo);

            try
            {
                await endpoint.ConnectAsync();

                IActiveContext activeContext = context.Activate(context.SessionInfo, new Hub.Session.Instance(endpoint));

                _messageMediator.Publish(new TransitionToStateMessage<IActiveContext>(Name.Synchronizing, activeContext));
            }
            catch
            {
                _messageMediator.Publish(new TransitionToStateMessage<IContext>(Name.Stopped, context));
            }
        }

        public void OnExit(ISessionContext context)
        {
            Instrumentation.State.ExitingState(Name.EstablishingSession);

            base.ExitStoppable(context);

            Instrumentation.State.ExitedState(Name.EstablishingSession);
        }
    }
}
