
namespace Bebbs.Harmonize.With.Messaging.Mapping
{
    internal class Observe : Message.IObserve
    {
        public Component.IIdentity Entity { get; set; }
        public Component.IIdentity Observable { get; set; }
        public Component.IIdentity Observer { get; set; }
    }
}
