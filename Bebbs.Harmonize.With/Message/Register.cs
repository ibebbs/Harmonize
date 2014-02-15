
namespace Bebbs.Harmonize.With.Message
{
    public interface IRegister
    {
        Component.IIdentity Registrar { get; }
        Component.IEntity Entity { get; }
    }

    public class Register : IRegister
    {
        public Register(Component.IIdentity registrar, Component.IEntity entity)
        {
            Registrar = registrar;
            Entity = entity;
        }

        public Component.IIdentity Registrar { get; private set; }
        public Component.IEntity Entity { get; private set; }
    }
}
