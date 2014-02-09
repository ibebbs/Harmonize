
namespace Bebbs.Harmonize.With.Component
{
    public interface IParameterValue
    {
        IIdentity Identity { get; }
        IMeasurement Measurement { get; }
    }
}
