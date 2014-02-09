
namespace Bebbs.Harmonize.With.Component
{
    public interface IValueDescription : IDescription
    {
        MeasurementType Measurement { get; }
        IMeasurement Minimum { get; }
        IMeasurement Maximum { get; }
        IMeasurement Default { get; }
    }
}
