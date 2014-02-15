
namespace Bebbs.Harmonize.With.Message
{
    public interface IDeregister
    {
        Component.IIdentity Registrar { get; }
        Component.IIdentity Entity { get; }
    }

    public class Deregister
    {
        public Deregister(Component.IIdentity registrar, Component.IIdentity entity)
        {
            Registrar = registrar;
            Device = entity;
        }

        public Component.IIdentity Registrar { get; private set; }
        public Component.IIdentity Device { get; private set; }
    }
}
