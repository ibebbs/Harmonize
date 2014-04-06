using System;

namespace Bebbs.Harmonize.State.Event
{
    public class Observed
    {
        public Component.Identity Entity { get; set; }
        public Component.Identity Observable { get; set; }
        public DateTimeOffset Date { get; set; }
        public Component.Measurement Measurement { get; set; }
    }
}
