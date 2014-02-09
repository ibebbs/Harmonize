
namespace Bebbs.Harmonize.With.Message
{
    public interface ISubscribe
    {
        Component.IIdentity Entity { get; }
        Component.IIdentity Observable { get; }
        Component.IIdentity Subscriber { get; }
    }

    public class Subscribe : ISubscribe
    {
        public Subscribe(Component.IIdentity entity, Component.IIdentity observable, Component.IIdentity subscriber)
        {
            Entity = entity;
            Observable = observable;
            Subscriber = subscriber;
        }

        public Component.IIdentity Entity { get; private set; }
        public Component.IIdentity Observable { get; private set; }
        public Component.IIdentity Subscriber { get; private set; }
    }
}
