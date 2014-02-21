using Bebbs.Harmonize.With.Harmony.Messages;

namespace Bebbs.Harmonize.With.Harmony.State
{
    internal class Starting : IState<IContext>
    {
        private readonly IGlobalEventAggregator _eventAggregator;
        private readonly IAsyncHelper _asyncHelper;

        public Starting(IGlobalEventAggregator eventAggregator, IAsyncHelper asyncHelper)
        {
            _eventAggregator = eventAggregator;
            _asyncHelper = asyncHelper;
        }

        public void OnEnter(IContext context)
        {
            EventSource.Log.EnteringState(Name.Starting);

            _eventAggregator.Publish(new TransitionToStateMessage<IContext>(Name.Authenticating, context));

            EventSource.Log.EnteredState(Name.Starting);
        }

        public void OnExit(IContext context)
        {
            EventSource.Log.ExitingState(Name.Starting);

            // Do nothing

            EventSource.Log.ExitedState(Name.Starting);
        }
    }
}
