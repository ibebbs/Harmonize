using System;
using Bebbs.Harmonize.With.Harmony.Messages;

namespace Bebbs.Harmonize.With.Harmony.State
{
    public abstract class StoppableState
    {
        private readonly Messages.IMediator _messageMediator;
        private readonly IAsyncHelper _asyncHelper;

        private IDisposable _subscription;

        public StoppableState(Messages.IMediator messageMediator, IAsyncHelper asyncHelper)
        {
            _messageMediator = messageMediator;
            _asyncHelper = asyncHelper;
        }

        protected abstract void Stop(IContext context, IStopHarmonizingMessage message);

        protected void EnterStoppable(IContext context)
        {
            _subscription = _messageMediator.GetEvent<IStopHarmonizingMessage>().Subscribe(message => Stop(context, message));
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
