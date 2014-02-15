
namespace Bebbs.Harmonize.With.Messaging.Mapping
{
    internal class ValueDescription : Description, Component.IValueDescription
    {
        public Component.MeasurementType Measurement { get; set; }
        public Component.IMeasurement Minimum { get; set; }
        public Component.IMeasurement Maximum { get; set; }
        public Component.IMeasurement Default { get; set; }
    }
}
