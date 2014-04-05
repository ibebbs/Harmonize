
namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.State.Event
{
    public class Transition
    {
        public Transition(Context.IContext context)
        {
            Context = context;
        }

        public Context.IContext Context { get; private set; }
    }
}
