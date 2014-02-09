
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
}
