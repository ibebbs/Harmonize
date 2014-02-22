using System.Collections.Generic;
using System.Linq;
using Bebbs.Harmonize.With.Harmony.Messages;

namespace Bebbs.Harmonize.With.Harmony.State
{
    internal class Registration : StoppableState, IState<IRegistrationContext>
    {
        private readonly IGlobalEventAggregator _eventAggregator;
        private readonly IAsyncHelper _asyncHelper;

        public Registration(IGlobalEventAggregator eventAggregator, IAsyncHelper asyncHelper) : base(eventAggregator, asyncHelper)
        {
            _eventAggregator = eventAggregator;
            _asyncHelper = asyncHelper;
        }

        protected override void Stop(IContext context, IStopHarmonizingMessage message)
        {
            _eventAggregator.Publish(new TransitionToStateMessage<IContext>(Name.Stopping, context));
        }

        private void RegisterDevices(IEnumerable<Hub.IEntity> devices)
        {
            (devices ?? Enumerable.Empty<Hub.IEntity>()).Cast<With.Component.IEntity>().Select(entity => new With.Message.Register(new With.Component.StringIdentity("Bebbs.Harmonize.Harmony"), entity)).ForEach(_eventAggregator.Publish);
        }

        public void OnEnter(IRegistrationContext context)
        {
            EventSource.Log.EnteringState(Name.Registration);

            base.EnterStoppable(context);

            RegisterDevices(context.HarmonyConfiguration.Devices);

            _eventAggregator.Publish(new TransitionToStateMessage<IRegistrationContext>(Name.Started, context));

            EventSource.Log.EnteredState(Name.Registration);
        }

        public void OnExit(IRegistrationContext context)
        {
            EventSource.Log.ExitingState(Name.Synchronizing);

            base.EnterStoppable(context);

            EventSource.Log.ExitedState(Name.Synchronizing);
        }
    }
}
