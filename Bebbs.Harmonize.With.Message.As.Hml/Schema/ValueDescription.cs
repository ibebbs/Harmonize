
namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class ValueDescription : Description, With.Component.IValueDescription
    {
        With.Component.MeasurementType With.Component.IValueDescription.Measurement
        {
            get { return (With.Component.MeasurementType)(int)Measurement; }
        }

        With.Component.IMeasurement With.Component.IValueDescription.Minimum
        {
            get { return Minimum; }
        }

        With.Component.IMeasurement With.Component.IValueDescription.Maximum
        {
            get { return Maximum; }
        }

        With.Component.IMeasurement With.Component.IValueDescription.Default
        {
            get { return Default; }
        }

        public MeasurementType Measurement { get; set; }
        public Measurement Minimum { get; set; }
        public Measurement Maximum { get; set; }
        public Measurement Default { get; set; }
    }
}
