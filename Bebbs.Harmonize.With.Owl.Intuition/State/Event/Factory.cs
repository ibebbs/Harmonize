
namespace Bebbs.Harmonize.With.Owl.Intuition.State.Event
{
    public interface IFactory
    {
        TransitionState ForStateTransition(Context.IContext context);
    }

    internal class Factory : IFactory
    {
        public TransitionState ForStateTransition(Context.IContext context)
        {
            return new TransitionState(context);
        }
    }
}
