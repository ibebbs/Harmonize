using Bebbs.Harmonize.With.Component;
using System;
using System.Reactive.Linq;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.Entity.Observable
{
    public class CurrentElectricityConsumption : IGatewayObservable
    {
        private const string ObservableIdentityPattern = "{0}-{1}";

        private static readonly IValueDescription ValueDescription = new ValueDescription(
            "Current Electricity Consumption",
            "Emits values representing the current electricity consumption",
            MeasurementType.Watt,
            new Measurement(MeasurementType.Watt, "0"),
            null,
            new Measurement(MeasurementType.Watt, "0")
        );

        private readonly Event.IMediator _eventMediator;
        private readonly IClock _clock;
        private readonly IInstance _entity;
        private readonly IIdentity _identity;

        private IDisposable _subscription;

        public CurrentElectricityConsumption(Event.IMediator eventMediator, IClock clock, IInstance entity)
        {
            _eventMediator = eventMediator;
            _clock = clock;
            _entity = entity;

            _identity = new StringIdentity(string.Format(ObservableIdentityPattern, _entity.Identity.ToString(), "CurrentElectricityConsumption"));
        }

        private void Process(Packet.Electricity reading)
        {
            _eventMediator.Publish(new Event.Observation(_entity.Identity, _identity, _clock.Now, new Measurement(MeasurementType.Watt, reading.Channels[0].Current.Value.ToString())));
        }

        public void Initialize()
        {
            _subscription = _eventMediator.GetEvent<Event.Reading>().Select(reading => reading.Value).OfType<Packet.Electricity>().Subscribe(Process);
        }

        public void Cleanup()
        {
            if (_subscription != null)
            {
                _subscription.Dispose();
                _subscription = null;
            }
        }

        public IIdentity Identity
        {
            get { return _identity; }
        }

        public IValueDescription Description
        {
            get { return ValueDescription; }
        }
    }
}
