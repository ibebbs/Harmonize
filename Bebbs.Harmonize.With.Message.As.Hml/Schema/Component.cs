
namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class Component : With.Component.IComponent
    {
        With.Component.IIdentity With.Component.IComponent.Identity 
        {
            get { return Identity; }
        }

        With.Component.IComponentDescription With.Component.IComponent.Description 
        {
            get { return Description; }
        }

        public Identity Identity { get; set; }
        public ComponentDescription Description { get; set; }
    }
}
