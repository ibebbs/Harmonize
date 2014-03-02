using System.Collections.Generic;
using System.Linq;
using Bebbs.Harmonize.With.Harmony.Messages;

namespace Bebbs.Harmonize.With.Harmony.State
{
    internal class Registration : StoppableState, IState<IRegistrationContext>
    {
        private readonly Messages.IMediator _messageMediator;
        private readonly IAsyncHelper _asyncHelper;

        public Registration(Messages.IMediator messageMediator, IAsyncHelper asyncHelper) : base(messageMediator, asyncHelper)
        {
            _messageMediator = messageMediator;
            _asyncHelper = asyncHelper;
        }

        protected override void Stop(IContext context, IStopHarmonizingMessage message)
        {
            _messageMediator.Publish(new TransitionToStateMessage<IContext>(Name.Stopping, context));
        }

        private void RegisterDevices(IEnumerable<Hub.IEntity> devices)
        {
            (devices ?? Enumerable.Empty<Hub.IEntity>()).Cast<With.Component.IEntity>().Select(entity => new With.Message.Register(new With.Component.StringIdentity("Bebbs.Harmonize.Harmony"), entity)).ForEach(_messageMediator.Publish);
        }

        public void OnEnter(IRegistrationContext context)
        {
            Instrumentation.State.EnteringState(Name.Registration);

            base.EnterStoppable(context);

            RegisterDevices(context.HarmonyConfiguration.Devices);

            _messageMediator.Publish(new TransitionToStateMessage<IRegistrationContext>(Name.Started, context));

            Instrumentation.State.EnteredState(Name.Registration);
        }

        public void OnExit(IRegistrationContext context)
        {
            Instrumentation.State.ExitingState(Name.Synchronizing);

            base.EnterStoppable(context);

            Instrumentation.State.ExitedState(Name.Synchronizing);
        }
    }
}
