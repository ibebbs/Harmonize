
namespace Bebbs.Harmonize.With.Messaging.Mapping
{
    internal class Deregister : Message.IDeregister
    {
        public Component.IIdentity Registrar { get; set; }
        public Component.IIdentity Entity { get; set; }
    }
}
