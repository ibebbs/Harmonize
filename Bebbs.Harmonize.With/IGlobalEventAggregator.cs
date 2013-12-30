using Reactive.EventAggregator;

namespace Bebbs.Harmonize.With
{
    public interface IGlobalEventAggregator : IEventAggregator { }

    public class GlobalEventAggregator : EventAggregator, IGlobalEventAggregator { }
}
