
namespace Bebbs.Harmonize.With.Messaging.Mapping
{
    internal class ParameterValue : Component.IParameterValue
    {
        public Component.IIdentity Identity { get; set; }
        public Component.IMeasurement Measurement { get; set; }
    }
}
