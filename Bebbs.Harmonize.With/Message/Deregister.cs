
namespace Bebbs.Harmonize.With.Message
{
    public interface IDeregister : IMessage
    {
        Component.IIdentity Registrar { get; }
        Component.IIdentity Entity { get; }
    }

    public class Deregister : IDeregister
    {
        public Deregister(Component.IIdentity registrar, Component.IIdentity entity)
        {
            Registrar = registrar;
            Entity = entity;
        }

        public Component.IIdentity Registrar { get; private set; }
        public Component.IIdentity Entity { get; private set; }
    }
}
