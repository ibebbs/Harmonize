
namespace Bebbs.Harmonize.With.Message
{
    public interface IDeregister
    {
        Component.IEntity Entity { get; }
    }

    public class Deregister
    {
        public Deregister(Component.IEntity entity)
        {
            Device = entity;
        }

        public Component.IEntity Device { get; private set; }
    }
}
