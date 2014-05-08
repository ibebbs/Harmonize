
namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class Observable : With.Component.IObservable
    {
        Component.IIdentity Component.IObservable.Identity
        {
            get { return Identity; }
        }

        Component.IValueDescription Component.IObservable.Description
        {
            get { return Description; }
        }

        public Identity Identity { get; set; }
        public ValueDescription Description { get; set; }
    }
}
