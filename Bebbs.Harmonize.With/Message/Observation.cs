using System;

namespace Bebbs.Harmonize.With.Message
{
    public interface IObservation
    {
        Component.IIdentity Entity { get; }
        Component.IIdentity Observable { get; }
        DateTimeOffset Date { get; }
        Component.IMeasurement Measurement { get; }
    }

    internal class Observation : IObservation
    {
        public Observation(Component.IIdentity entity, Component.IIdentity observable, DateTimeOffset date, Component.IMeasurement measurement)
        {
            Entity = entity;
            Observable = observable;
            Date = date;
            Measurement = measurement;
        }
        
        public Component.IIdentity Entity { get; private set; }
        public Component.IIdentity Observable { get; private set; }
        public DateTimeOffset Date { get; private set; }
        public Component.IMeasurement Measurement { get; private set; }
    }
}
