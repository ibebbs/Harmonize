
namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class ParameterDescription : Description, With.Component.IParameterDescription
    {
        With.Component.MeasurementType With.Component.IParameterDescription.Measurement
        {
            get { return (With.Component.MeasurementType)(int)Measurement; }
        }

        With.Component.IMeasurement With.Component.IParameterDescription.Minimum
        {
            get { return Minimum; }
        }

        With.Component.IMeasurement With.Component.IParameterDescription.Maximum
        {
            get { return Maximum; }
        }

        With.Component.IMeasurement With.Component.IParameterDescription.Default
        {
            get { return Default; }
        }

        bool With.Component.IParameterDescription.Required
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
