
namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class Parameter : With.Component.IParameter
    {
        With.Component.IIdentity With.Component.IParameter.Identity
        {
            get { return Identity; }
        }

        With.Component.IParameterDescription With.Component.IParameter.Description
        {
            get { return Description; }
        }

        public Identity Identity { get; set; }
        public ParameterDescription Description { get; set; }
    }
}
