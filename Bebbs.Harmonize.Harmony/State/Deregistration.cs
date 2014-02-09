using Bebbs.Harmonize.Harmony.Messages;
using Bebbs.Harmonize.With;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.Harmony.State
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

        private void DeregisterDevices(IEnumerable<Hub.IDevice> devices)
        {
            (devices ?? Enumerable.Empty<Hub.IDevice>()).Cast<With.Component.IEntity>().Select(entity => new With.Message.Deregister(entity)).ForEach(_eventAggregator.Publish);
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
