
namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class Add : Message, With.Message.IAdd
    {
        With.Component.IIdentity With.Message.IAdd.Registrar
        {
            get { return Registrar; }
        }

        With.Component.IComponent With.Message.IAdd.Component 
        {
            get { return Component; }
        }

        public Identity Registrar { get; set; }

        public Component Component { get; set; }
    }
}
