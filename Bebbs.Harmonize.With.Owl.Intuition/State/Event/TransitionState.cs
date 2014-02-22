
namespace Bebbs.Harmonize.With.Owl.Intuition.State.Event
{
    public class TransitionState
    {
        public TransitionState(Context.IContext context)
        {
            Context = context;
        }

        public Context.IContext Context { get; private set; }
    }
}
