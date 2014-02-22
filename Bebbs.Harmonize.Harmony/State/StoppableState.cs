using System;
using Bebbs.Harmonize.With.Harmony.Messages;

namespace Bebbs.Harmonize.With.Harmony.State
{
    public abstract class StoppableState
    {
        private readonly IGlobalEventAggregator _eventAggregator;
        private readonly IAsyncHelper _asyncHelper;

        private IDisposable _subscription;

        public StoppableState(IGlobalEventAggregator eventAggregator, IAsyncHelper asyncHelper)
        {
            _eventAggregator = eventAggregator;
            _asyncHelper = asyncHelper;
        }

        protected abstract void Stop(IContext context, IStopHarmonizingMessage message);

        protected void EnterStoppable(IContext context)
        {
            _subscription = _eventAggregator.GetEvent<IStopHarmonizingMessage>().Subscribe(message => Stop(context, message));
        }

        protected void ExitStoppable(IContext context)
        {
            if (_subscription != null)
            {
                _subscription.Dispose();
                _subscription = null;
            }
        }
    }
}
