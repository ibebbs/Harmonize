namespace Bebbs.Harmonize.With.Harmony.State
{
    public class Faulted : IState<IFaultedContext>
    {
        private readonly IGlobalEventAggregator _eventAggregator;
        private readonly IAsyncHelper _asyncHelper;

        public Faulted(IGlobalEventAggregator eventAggregator, IAsyncHelper asyncHelper)
        {
            _eventAggregator = eventAggregator;
            _asyncHelper = asyncHelper;
        }

        public void OnEnter(IFaultedContext context)
        {
            _eventAggregator.Publish(new Messages.ErrorMessage(context.Exception));
        }

        public void OnExit(IFaultedContext context)
        {
            // Do nothing
        }
    }
}
