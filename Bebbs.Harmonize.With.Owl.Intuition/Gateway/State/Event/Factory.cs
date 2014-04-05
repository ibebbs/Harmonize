
namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.State.Event
{
    public interface IFactory
    {
        Transition ForStateTransition(Context.IContext context);
    }

    internal class Factory : IFactory
    {
        public Transition ForStateTransition(Context.IContext context)
        {
            return new Transition(context);
        }
    }
}
