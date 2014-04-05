using Bebbs.Harmonize.With.Component;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.Event
{
    public class Deregister
    {
        public Deregister(IEntity entity)
        {
            Entity = entity;
        }

        public IEntity Entity { get; private set; }
    }
}
