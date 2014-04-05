
namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.State
{
    internal class Faulted : IState
    {
        private Gateway.Event.IMediator _eventMediator;
        private ITransition _transition;
        private Context.IFault _context;

        public Faulted(Gateway.Event.IMediator eventMediator, ITransition transition, Context.IFault context)
        {
            _eventMediator = eventMediator;
            _transition = transition;
            _context = context;
        }

        public void OnEnter()
        {
            _eventMediator.Publish(new Gateway.Event.Errored(_context.Exception));
        }

        public void OnExit()
        {
            // Do nothing
        }

        public Name Name
        {
            get { return Name.Faulted; }
        }
    }
}
