
namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class Measurement : With.Component.IMeasurement
    {
        With.Component.MeasurementType With.Component.IMeasurement.Type
        {
            get { return (With.Component.MeasurementType)(int)Type; }
        }

        string With.Component.IMeasurement.Value
        {
            get { return Value; }
        }

        public MeasurementType Type { get; set; }
        public string Value { get; set; }
    }
}
