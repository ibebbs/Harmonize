
namespace Bebbs.Harmonize.With.Message
{
    public interface IRemove : IMessage
    {
        Component.IIdentity Registrar { get; }
        Component.IIdentity Component { get; }
    }

    public class Remove : IRemove
    {
        public Remove(Component.IIdentity registrar, Component.IIdentity component)
        {
            Registrar = registrar;
            Component = component;
        }

        public Component.IIdentity Registrar { get; private set; }
        public Component.IIdentity Component { get; private set; }
    }
}
