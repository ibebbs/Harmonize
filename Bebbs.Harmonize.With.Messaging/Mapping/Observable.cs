
namespace Bebbs.Harmonize.With.Messaging.Mapping
{
    internal class Observable : Component.IObservable
    {
        public Component.IIdentity Identity { get; set; }
        public Component.IValueDescription Description { get; set; }
    }
}
