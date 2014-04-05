using Bebbs.Harmonize.With.Component;

namespace Bebbs.Harmonize.State.Component
{
    public class ValueDescription : Description
    {
        public MeasurementType Measurement { get; set; }
        public Measurement Minimum { get; set; }
        public Measurement Maximum { get; set; }
        public Measurement Default { get; set; }
    }
}
