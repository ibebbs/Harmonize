using System;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Common.Dto
{
    public class Observation : Message
    {
        public Identity Entity { get; set; }
        public Identity Observable { get; set; }
        public DateTimeOffset Date { get; set; }
        public Measurement Measurement { get; set; }
    }
}
