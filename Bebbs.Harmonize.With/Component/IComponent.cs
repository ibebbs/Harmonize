
namespace Bebbs.Harmonize.With.Component
{
    public interface IComponent
    {
        IIdentity Identity { get; }
        IComponentDescription Description { get; }
    }

    public class Component : IComponent
    {
        public Component(IIdentity identity, IComponentDescription description)
        {
            Identity = identity;
            Description = description;
        }

        public IIdentity Identity { get; private set; }
        public IComponentDescription Description { get; private set; }
    }
}
