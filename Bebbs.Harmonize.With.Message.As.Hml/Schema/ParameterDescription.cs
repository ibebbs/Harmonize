
namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class ParameterDescription : Description, With.Component.IParameterDescription
    {
        Component.MeasurementType Component.IParameterDescription.Measurement
        {
            get { return (Component.MeasurementType)(int)Measurement; }
        }

        Component.IMeasurement Component.IParameterDescription.Minimum
        {
            get { return Minimum; }
        }

        Component.IMeasurement Component.IParameterDescription.Maximum
        {
            get { return Maximum; }
        }

        Component.IMeasurement Component.IParameterDescription.Default
        {
            get { return Default; }
        }

        bool Component.IParameterDescription.Required
        {
            get { return Required; }
        }

        public MeasurementType Measurement { get; set; }
        public Measurement Minimum { get; set; }
        public Measurement Maximum { get; set; }
        public Measurement Default { get; set; }
        public bool Required { get; set; }
    }
}
