using Reactive.EventAggregator;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.Event
{
    public interface IMediator : IEventAggregator { }

    internal class Mediator : IMediator 
    {
        private readonly EventAggregator _aggregator;
        private readonly EventLoopScheduler _scheduler;

        public Mediator()
        {
            _aggregator = new EventAggregator();
            _scheduler = new EventLoopScheduler();
        }

        public IObservable<TEvent> GetEvent<TEvent>()
        {
            return _aggregator.GetEvent<TEvent>().ObserveOn(_scheduler);
        }

        public void Publish<TEvent>(TEvent sampleEvent)
        {
            _aggregator.Publish(sampleEvent);
        }
    }
}
