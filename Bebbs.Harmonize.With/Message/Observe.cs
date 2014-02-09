
namespace Bebbs.Harmonize.With.Message
{
    public interface IObserve
    {
        Component.IIdentity Entity { get; }
        Component.IIdentity Observable { get; }
        Component.IIdentity Observer { get; }
    }

    public class Observe : IObserve
    {
        public Observe(Component.IIdentity entity, Component.IIdentity observable, Component.IIdentity observer)
        {
            Entity = entity;
            Observable = observable;
            Observer = observer;
        }

        public Component.IIdentity Entity { get; private set; }
        public Component.IIdentity Observable { get; private set; }
        public Component.IIdentity Observer { get; private set; }
    }
}
