
namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.Entity
{
    public class ValueDescription : Component.IValueDescription
    {
        public ValueDescription(string name, string remarks, Component.MeasurementType measurement, Component.IMeasurement minimum, Component.IMeasurement maximum, Component.IMeasurement @default)
        {
            Name = name;
            Remarks = remarks;
            Measurement = measurement;
            Minimum = minimum;
            Maximum = maximum;
            Default = @default;
        }

        public string Name { get; private set; }

        public string Remarks { get; private set; }

        public Component.MeasurementType Measurement { get; private set; }

        public Component.IMeasurement Minimum { get; private set; }

        public Component.IMeasurement Maximum { get; private set; }

        public Component.IMeasurement Default { get; private set; }
    }
}
