using Bebbs.Harmonize.Common;
using Bebbs.Harmonize.Harmony.Messages;
using System;
using System.Reactive.Linq;

namespace Bebbs.Harmonize.Harmony.State
{
    internal class Synchronizing : StoppableState, IState<IActiveContext>
    {
        private readonly IGlobalEventAggregator _eventAggregator;
        private readonly IAsyncHelper _asyncHelper;
        private IDisposable _subscription;

        public Synchronizing(IGlobalEventAggregator eventAggregator, IAsyncHelper asyncHelper) : base(eventAggregator, asyncHelper)
        {
            _eventAggregator = eventAggregator;
            _asyncHelper = asyncHelper;
        }

        private void ProcessResponse(IActiveContext context, IHarmonyConfigurationResponseMessage message)
        {
            //TODO: Do something with the configuration
            //ISessionContext sessionContext = context.ForSession(response.SessionInfo);

            _eventAggregator.Publish(new TransitionToStateMessage<IActiveContext>(Name.Started, context));
        }

        private void ProcessException(IActiveContext context, Exception exception)
        {
            _eventAggregator.Publish(new TransitionToStateMessage<IContext>(Name.Stopped, context));
        }

        protected override void Stop(IContext context, IStopHarmonizingMessage message)
        {
            _eventAggregator.Publish(new TransitionToStateMessage<IContext>(Name.Stopping, context));
        }

        public void OnEnter(IActiveContext context)
        {
            EventSource.Log.EnteringState(Name.Synchronizing);

            base.EnterStoppable(context);

            _subscription = _eventAggregator.GetEvent<IHarmonyConfigurationResponseMessage>().Timeout(TimeSpan.FromSeconds(30)).Subscribe(response => ProcessResponse(context, response), exception => ProcessException(context, exception));

            _eventAggregator.Publish(new RequestHarmonyConfigurationMessage(context.Session));

            EventSource.Log.EnteredState(Name.Synchronizing);
        }

        public void OnExit(IActiveContext context)
        {
            EventSource.Log.ExitingState(Name.Synchronizing);

            if (_subscription != null)
            {
                _subscription.Dispose();
                _subscription = null;
            }

            base.EnterStoppable(context);

            EventSource.Log.ExitedState(Name.Synchronizing);
        }
    }
}
