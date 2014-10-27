
namespace Bebbs.Harmonize.With.Component
{
    public interface IParameterDescription : IDescription
    {
        MeasurementType Measurement { get; }
        IMeasurement Minimum { get; }
        IMeasurement Maximum { get; }
        IMeasurement Default { get; }
        bool Required { get; }
    }

    public class ParameterDescription : Description, IParameterDescription
    {
        public MeasurementType Measurement { get; set; }

        public IMeasurement Minimum { get; set; }

        public IMeasurement Maximum { get; set; }

        public IMeasurement Default { get; set; }

        public bool Required { get; set; }
    }

}
