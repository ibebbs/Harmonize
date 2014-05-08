
namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class Measurement : With.Component.IMeasurement
    {
        Component.MeasurementType Component.IMeasurement.Type
        {
            get { return (Component.MeasurementType)(int)Type; }
        }

        string Component.IMeasurement.Value
        {
            get { return Value; }
        }

        public MeasurementType Type { get; set; }
        public string Value { get; set; }
    }
}
