using EasyNetQ;
using System;
using System.Reactive.Linq;

namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq
{
    internal class Endpoint : IMessageEndpoint
    {
        private static readonly string HarmonizeSubscriptionId = "Bebbs.Harmonize.With.Messaging.Over.RabbitMq";
        private static readonly string RabbitMqConnectionString = "host=192.168.1.22";

        private readonly IGlobalEventAggregator _eventAggregator;

        private IBus _bus;

        public Endpoint(IGlobalEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Initialize()
        {
            _bus = RabbitHutch.CreateBus(RabbitMqConnectionString);
        }

        public void Cleanup()
        {
            if (_bus != null)
            {
                _bus.Dispose();
                _bus = null;
            }
        }

        public void Publish(Schema.Message message)
        {
            _bus.Publish(message);
        }

        public IObservable<Schema.Message> Messages
        {
            get { return Observable.Create<Schema.Message>(observer => _bus.Subscribe<Schema.Message>(HarmonizeSubscriptionId, observer.OnNext)); }
        }
    }
}
