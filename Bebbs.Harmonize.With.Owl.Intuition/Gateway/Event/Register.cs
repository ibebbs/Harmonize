using Bebbs.Harmonize.With.Component;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.Event
{
    public class Register
    {
        public Register(IEntity entity)
        {
            Entity = entity;
        }

        public IEntity Entity { get; private set; }
    }
}
