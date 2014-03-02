namespace Bebbs.Harmonize.With.Harmony.State
{
    public class Faulted : IState<IFaultedContext>
    {
        private readonly Messages.IMediator _messageMediator;
        private readonly IAsyncHelper _asyncHelper;

        public Faulted(Messages.IMediator messageMediator, IAsyncHelper asyncHelper)
        {
            _messageMediator = messageMediator;
            _asyncHelper = asyncHelper;
        }

        public void OnEnter(IFaultedContext context)
        {
            _messageMediator.Publish(new Messages.ErrorMessage(context.Exception));
        }

        public void OnExit(IFaultedContext context)
        {
            // Do nothing
        }
    }
}
