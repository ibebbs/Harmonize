
namespace Bebbs.Harmonize.With.Messaging.Mapping
{
    internal class ParameterDescription : Description, Component.IParameterDescription
    {
        public Component.MeasurementType Measurement { get; set; }
        public Component.IMeasurement Minimum { get; set; }
        public Component.IMeasurement Maximum { get; set; }
        public Component.IMeasurement Default { get; set; }
        public bool Required { get; set; }
    }
}
