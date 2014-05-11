
namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class ParameterValue : With.Component.IParameterValue
    {
        With.Component.IIdentity With.Component.IParameterValue.Identity
        {
            get { return Identity; }
        }

        With.Component.IMeasurement With.Component.IParameterValue.Measurement
        {
            get { return Measurement; }
        }

        public Identity Identity { get; set; }
        public Measurement Measurement { get; set; }
    }
}
