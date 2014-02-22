using System.Collections.Generic;
using System.Linq;
using Bebbs.Harmonize.With.Harmony.Messages;

namespace Bebbs.Harmonize.With.Harmony.State
{
    public class Deregistration : IState<IRegistrationContext>
    {
        private readonly IGlobalEventAggregator _eventAggregator;
        private readonly IAsyncHelper _asyncHelper;

        public Deregistration(IGlobalEventAggregator eventAggregator, IAsyncHelper asyncHelper)
        {
            _eventAggregator = eventAggregator;
            _asyncHelper = asyncHelper;
        }

        private void DeregisterDevices(IEnumerable<Hub.IEntity> devices)
        {
            (devices ?? Enumerable.Empty<Hub.IEntity>()).Cast<With.Component.IEntity>().Select(entity => new With.Message.Deregister(new With.Component.StringIdentity("Bebbs.Harmonize.Harmony"), entity.Identity)).ForEach(_eventAggregator.Publish);
        }

        public void OnEnter(IRegistrationContext context)
        {
            EventSource.Log.EnteringState(Name.Deregistration);

            DeregisterDevices(context.HarmonyConfiguration.Devices);

            _eventAggregator.Publish(new TransitionToStateMessage<IContext>(Name.Stopping, context));

            EventSource.Log.EnteredState(Name.Deregistration);
        }

        public void OnExit(IRegistrationContext context)
        {
            EventSource.Log.ExitingState(Name.Deregistration);

            // Do nothing

            EventSource.Log.ExitedState(Name.Deregistration);
        }
    }
}
