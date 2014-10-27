using Reactive.EventAggregator;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.Event
{
    public interface IMediator : IEventAggregator { }

    internal class Mediator : IMediator 
    {
        private EventAggregator _aggregator;
        private EventLoopScheduler _scheduler;

        public Mediator()
        {
            _aggregator = new EventAggregator();
            _scheduler = new EventLoopScheduler();
        }

        public void Dispose()
        {
            if (_aggregator != null)
            {
                _aggregator.Dispose();
                _aggregator = null;
            }

            if (_scheduler != null)
            {
                _scheduler.Dispose();
                _scheduler = null;
            }
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
