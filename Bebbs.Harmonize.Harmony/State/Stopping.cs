using Bebbs.Harmonize.With.Harmony.Messages;

namespace Bebbs.Harmonize.With.Harmony.State
{
    public class Stopping : IState<IContext>
    {
        private readonly IGlobalEventAggregator _eventAggregator;
        private readonly IAsyncHelper _asyncHelper;

        public Stopping(IGlobalEventAggregator eventAggregator, IAsyncHelper asyncHelper)
        {
            _eventAggregator = eventAggregator;
            _asyncHelper = asyncHelper;
        }

        public void OnEnter(IContext context)
        {
            EventSource.Log.EnteringState(Name.Stopping);

            _eventAggregator.Publish(new TransitionToStateMessage<IContext>(Name.Stopped, context));

            EventSource.Log.EnteredState(Name.Stopping);
        }

        public void OnExit(IContext context)
        {
            EventSource.Log.ExitingState(Name.Stopping);

            // Do nothing

            EventSource.Log.ExitedState(Name.Stopping);
        }
    }
}
