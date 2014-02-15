
namespace Bebbs.Harmonize.With.Messaging.Mapping
{
    internal class Parameter : Component.IParameter
    {
        public Component.IIdentity Identity { get; set; }
        public Component.IParameterDescription Description { get; set; }
    }
}
