
namespace Bebbs.Harmonize.With.Messaging.Mapping
{
    internal class Measurement : Component.IMeasurement
    {
        public Component.MeasurementType Type { get; set; }
        public string Value { get; set; }
    }
}
