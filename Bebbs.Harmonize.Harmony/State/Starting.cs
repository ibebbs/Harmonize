using Bebbs.Harmonize.With.Harmony.Messages;

namespace Bebbs.Harmonize.With.Harmony.State
{
    internal class Starting : IState<IContext>
    {
        private readonly Messages.IMediator _messageMediator;
        private readonly IAsyncHelper _asyncHelper;

        public Starting(Messages.IMediator messageMediator, IAsyncHelper asyncHelper)
        {
            _messageMediator = messageMediator;
            _asyncHelper = asyncHelper;
        }

        public void OnEnter(IContext context)
        {
            Instrumentation.State.EnteringState(Name.Starting);

            _messageMediator.Publish(new TransitionToStateMessage<IContext>(Name.Authenticating, context));

            Instrumentation.State.EnteredState(Name.Starting);
        }

        public void OnExit(IContext context)
        {
            Instrumentation.State.ExitingState(Name.Starting);

            // Do nothing

            Instrumentation.State.ExitedState(Name.Starting);
        }
    }
}
