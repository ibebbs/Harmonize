
namespace Bebbs.Harmonize.With.Message
{
    public interface IAdd : IMessage
    {
        Component.IIdentity Registrar { get; }
        Component.IComponent Component { get; }
    }

    public class Add : IAdd
    {
        public Add(Component.IIdentity registrar, Component.IComponent component)
        {
            Registrar = registrar;
            Component = component;
        }

        public Component.IIdentity Registrar { get; private set; }
        public Component.IComponent Component { get; private set; }
    }
}
