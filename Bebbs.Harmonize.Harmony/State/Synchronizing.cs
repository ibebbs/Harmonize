using System;
using System.Reactive.Linq;
using Bebbs.Harmonize.With.Harmony.Messages;

namespace Bebbs.Harmonize.With.Harmony.State
{
    internal class Synchronizing : StoppableState, IState<IActiveContext>
    {
        private readonly Messages.IMediator _messageMediator;
        private readonly IAsyncHelper _asyncHelper;
        private IDisposable _subscription;

        public Synchronizing(Messages.IMediator messageMediator, IAsyncHelper asyncHelper) : base(messageMediator, asyncHelper)
        {
            _messageMediator = messageMediator;
            _asyncHelper = asyncHelper;
        }

        protected override void Stop(IContext context, IStopHarmonizingMessage message)
        {
            _messageMediator.Publish(new TransitionToStateMessage<IContext>(Name.Stopping, context));
        }

        public void OnEnter(IActiveContext context)
        {
            Instrumentation.State.EnteringState(Name.Synchronizing);

            base.EnterStoppable(context);

            GetConfiguration(context);

            Instrumentation.State.EnteredState(Name.Synchronizing);
        }

        private async void GetConfiguration(IActiveContext context)
        {
            try
            {
                Hub.Configuration.IValues configuration = await context.Session.Endpoint.GetHarmonyConfigurationAsync();

                IRegistrationContext registrationContext = context.ForRegistration(configuration);

                _messageMediator.Publish(new TransitionToStateMessage<IRegistrationContext>(Name.Registration, registrationContext));
            }
            catch
            {
                _messageMediator.Publish(new TransitionToStateMessage<IContext>(Name.Stopped, context));
            }
        }

        public void OnExit(IActiveContext context)
        {
            Instrumentation.State.ExitingState(Name.Synchronizing);

            if (_subscription != null)
            {
                _subscription.Dispose();
                _subscription = null;
            }

            base.EnterStoppable(context);

            Instrumentation.State.ExitedState(Name.Synchronizing);
        }
    }
}
