
namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class Deregister : Message, With.Message.IDeregister
    {
        Component.IIdentity IDeregister.Registrar
        {
            get { return Registrar; }
        }

        Component.IIdentity IDeregister.Entity
        {
            get { return Entity; }
        }

        public Identity Registrar { get; set; }
        public Identity Entity { get; set; }

    }
}
