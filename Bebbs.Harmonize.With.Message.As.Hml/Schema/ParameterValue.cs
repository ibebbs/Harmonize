
namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class ParameterValue : With.Component.IParameterValue
    {
        Component.IIdentity Component.IParameterValue.Identity
        {
            get { return Identity; }
        }

        Component.IMeasurement Component.IParameterValue.Measurement
        {
            get { return Measurement; }
        }

        public Identity Identity { get; set; }
        public Measurement Measurement { get; set; }
    }
}
