namespace Bebbs.Harmonize.With.Harmony.Messages
{
    public interface ITransitionToStateMessage : IMessage
    {
        State.Name State { get; }
    }

    public interface ITransitionToStateMessage<T> : ITransitionToStateMessage where T : IContext
    {
        T Context { get; }
    }

    public class TransitionToStateMessage<T> : ITransitionToStateMessage<T> where T : IContext
    {
        public TransitionToStateMessage(State.Name state, T context)
        {
            State = state;
            Context = context;
        }

        public State.Name State { get; private set; }

        public T Context { get; private set; }
    }
}
