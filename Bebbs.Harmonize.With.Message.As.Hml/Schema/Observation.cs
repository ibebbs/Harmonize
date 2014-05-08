using System;

namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class Observation : Message, With.Message.IObservation
    {
        Component.IIdentity IObservation.Entity
        {
            get { return Entity; }
        }

        Component.IIdentity IObservation.Observable
        {
            get { return Observable; }
        }

        DateTimeOffset IObservation.Date
        {
            get { return Date; }
        }

        Component.IMeasurement IObservation.Measurement
        {
            get { return Measurement; }
        }

        public Identity Entity { get; set; }
        public Identity Observable { get; set; }
        public DateTimeOffset Date { get; set; }
        public Measurement Measurement { get; set; }
    }
}
