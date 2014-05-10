
using System.Xml.Serialization;
namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    
    [XmlInclude(typeof(Action))]
    [XmlInclude(typeof(Deregister))]
    [XmlInclude(typeof(Ignore))]
    [XmlInclude(typeof(Observation))]
    [XmlInclude(typeof(Observe))]
    [XmlInclude(typeof(Register))]
    public class Message : IMessage
    {
    }
}
