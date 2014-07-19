
namespace Bebbs.Harmonize.With.Component
{
    public interface IMeasurement
    {
        MeasurementType Type { get; }
        string Value { get; }
    }

    public class Measurement : IMeasurement
    {
        public Measurement(MeasurementType type, string value)
        {
            Type = type;
            Value = value;
        }

        public MeasurementType Type { get; private set; }
        public string Value { get; private set; }
    }
}
