
namespace Bebbs.Harmonize.With.Messaging.Mapping
{
    internal class Subscribe : Message.ISubscribe
    {
        public Component.IIdentity Entity { get; set; }
        public Component.IIdentity Observable { get; set; }
        public Component.IIdentity Subscriber { get; set; }
    }
}
