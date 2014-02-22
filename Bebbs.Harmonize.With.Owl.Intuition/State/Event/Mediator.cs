using Reactive.EventAggregator;

namespace Bebbs.Harmonize.With.Owl.Intuition.State.Event
{
    public interface IMediator : IEventAggregator { }

    internal class Mediator : EventAggregator, IMediator { }
}
