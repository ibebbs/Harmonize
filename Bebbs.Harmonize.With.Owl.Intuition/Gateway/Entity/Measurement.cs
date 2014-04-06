
namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.Entity
{
    public class Measurement : Component.IMeasurement
    {
        public Measurement(Component.MeasurementType @type, string value)
        {
            Type = type;
            Value = value;
        }

        public Component.MeasurementType Type { get; private set; }
        public string Value { get; private set; }
    }
}
