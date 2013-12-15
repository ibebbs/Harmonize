using EventSourceProxy;

namespace Bebbs.Harmonize.Harmony
{
    [EventSourceImplementation(Name = "Cogenity-Harmonize-Harmony")]
    public interface IEventSource
    {
        void EnteringState(State.Name state);

        void EnteredState(State.Name state);

        void ExitingState(State.Name state);

        void ExitedState(State.Name state);
    }

    public static class EventSource
    {
        public static readonly IEventSource Log = EventSourceImplementer.GetEventSourceAs<IEventSource>();
    }
}
