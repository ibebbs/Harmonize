
namespace Bebbs.Harmonize.With.Message
{
    public interface IRegister
    {
        Component.IEntity Entity { get; }
    }

    public class Register : IRegister
    {
        public Register(Component.IEntity entity)
        {
            Entity = entity;
        }

        public Component.IEntity Entity { get; private set; }
    }
}
