using Bebbs.Harmonize.With.Harmony.Messages;

namespace Bebbs.Harmonize.With.Harmony.State
{
    public class Stopping : IState<IContext>
    {
        private readonly Messages.IMediator _messageMediator;
        private readonly IAsyncHelper _asyncHelper;

        public Stopping(Messages.IMediator messageMediator, IAsyncHelper asyncHelper)
        {
            _messageMediator = messageMediator;
            _asyncHelper = asyncHelper;
        }

        public void OnEnter(IContext context)
        {
            Instrumentation.State.EnteringState(Name.Stopping);

            _messageMediator.Publish(new TransitionToStateMessage<IContext>(Name.Stopped, context));

            Instrumentation.State.EnteredState(Name.Stopping);
        }

        public void OnExit(IContext context)
        {
            Instrumentation.State.ExitingState(Name.Stopping);

            // Do nothing

            Instrumentation.State.ExitedState(Name.Stopping);
        }
    }
}
