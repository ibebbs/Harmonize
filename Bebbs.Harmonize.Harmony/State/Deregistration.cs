using System.Collections.Generic;
using System.Linq;
using Bebbs.Harmonize.With.Harmony.Messages;

namespace Bebbs.Harmonize.With.Harmony.State
{
    public class Deregistration : IState<IRegistrationContext>
    {
        private readonly Messages.IMediator _messageMediator;
        private readonly IAsyncHelper _asyncHelper;

        public Deregistration(Messages.IMediator messageMediator, IAsyncHelper asyncHelper)
        {
            _messageMediator = messageMediator;
            _asyncHelper = asyncHelper;
        }

        private void DeregisterDevices(IEnumerable<Hub.IEntity> devices)
        {
            (devices ?? Enumerable.Empty<Hub.IEntity>()).Cast<With.Component.IEntity>().Select(entity => new With.Message.Deregister(new With.Component.StringIdentity("Bebbs.Harmonize.Harmony"), entity.Identity)).ForEach(_messageMediator.Publish);
        }

        public void OnEnter(IRegistrationContext context)
        {
            Instrumentation.State.EnteringState(Name.Deregistration);

            DeregisterDevices(context.HarmonyConfiguration.Devices);

            _messageMediator.Publish(new TransitionToStateMessage<IContext>(Name.Stopping, context));

            Instrumentation.State.EnteredState(Name.Deregistration);
        }

        public void OnExit(IRegistrationContext context)
        {
            Instrumentation.State.ExitingState(Name.Deregistration);

            // Do nothing

            Instrumentation.State.ExitedState(Name.Deregistration);
        }
    }
}
