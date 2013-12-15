using Reactive.EventAggregator;

namespace Bebbs.Harmonize
{
    public interface IGlobalEventAggregator : IEventAggregator { }

    public class GlobalEventAggregator : EventAggregator, IGlobalEventAggregator { }
}
