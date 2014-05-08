
namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class Register : Message, With.Message.IRegister
    {
        Component.IIdentity IRegister.Registrar
        {
            get { return Registrar; }
        }

        Component.IEntity IRegister.Entity
        {
            get { return Entity; }
        }

        public Identity Registrar { get; set; }
        public Entity Entity { get; set; }
    }
}
