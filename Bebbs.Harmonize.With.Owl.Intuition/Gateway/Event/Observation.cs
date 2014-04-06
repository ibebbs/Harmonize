using System;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.Event
{
    public class Observation
    {
        public Observation(Component.IIdentity entityIdentity, Component.IIdentity observableIdentity, DateTimeOffset asOf, Component.IMeasurement measurement)
        {
            EntityIdentity = entityIdentity;
            ObservableIdentity = observableIdentity;
            AsOf = asOf;
            Measurement = measurement;
        }

        public Component.IIdentity EntityIdentity { get; private set; }

        public Component.IIdentity ObservableIdentity { get; private set; }

        public DateTimeOffset AsOf { get; private set; }

        public Component.IMeasurement Measurement { get; private set; }
    }
}
