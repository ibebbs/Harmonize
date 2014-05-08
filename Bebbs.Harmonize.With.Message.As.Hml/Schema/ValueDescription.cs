
namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class ValueDescription : Description, With.Component.IValueDescription
    {
        Component.MeasurementType Component.IValueDescription.Measurement
        {
            get { return (Component.MeasurementType)(int)Measurement; }
        }

        Component.IMeasurement Component.IValueDescription.Minimum
        {
            get { return Minimum; }
        }

        Component.IMeasurement Component.IValueDescription.Maximum
        {
            get { return Maximum; }
        }

        Component.IMeasurement Component.IValueDescription.Default
        {
            get { return Default; }
        }

        public MeasurementType Measurement { get; set; }
        public Measurement Minimum { get; set; }
        public Measurement Maximum { get; set; }
        public Measurement Default { get; set; }
    }
}
