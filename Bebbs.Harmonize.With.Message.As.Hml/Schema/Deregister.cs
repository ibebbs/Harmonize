
namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class Deregister : Message, With.Message.IDeregister
    {
        With.Component.IIdentity IDeregister.Registrar
        {
            get { return Registrar; }
        }

        With.Component.IIdentity IDeregister.Entity
        {
            get { return Entity; }
        }

        public Identity Registrar { get; set; }
        public Identity Entity { get; set; }

    }
}
