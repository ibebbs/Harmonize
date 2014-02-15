
namespace Bebbs.Harmonize.With.Messaging.Mapping
{
    internal class Register : Message.IRegister
    {
        public Component.IIdentity Registrar { get; set; }
        public Component.IEntity Entity { get; set; }
    }
}
