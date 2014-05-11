
namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class Remove : Message, IRemove
    {
        With.Component.IIdentity IRemove.Registrar 
        {
            get { return Registrar; }
        }

        With.Component.IIdentity IRemove.Component 
        {
            get { return Component; }
        }

        public Identity Registrar { get; set; }
        public Identity Component { get; set; }
    }
}
