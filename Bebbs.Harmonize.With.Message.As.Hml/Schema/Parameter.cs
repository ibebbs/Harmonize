
namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class Parameter : With.Component.IParameter
    {
        Component.IIdentity Component.IParameter.Identity
        {
            get { return Identity; }
        }

        Component.IParameterDescription Component.IParameter.Description
        {
            get { return Description; }
        }

        public Identity Identity { get; set; }
        public ParameterDescription Description { get; set; }
    }
}
