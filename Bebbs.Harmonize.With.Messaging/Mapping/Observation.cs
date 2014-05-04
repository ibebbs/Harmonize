using System;

namespace Bebbs.Harmonize.With.Messaging.Mapping
{
    internal class Observation : Message.IObservation
    {
        public Component.IIdentity Entity { get; set; }

        public Component.IIdentity Observable { get; set; }

        public DateTimeOffset Date { get; set; }

        public Component.IMeasurement Measurement { get; set; }
    }
}
