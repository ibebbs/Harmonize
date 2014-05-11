
namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class Observable : With.Component.IObservable
    {
        With.Component.IIdentity With.Component.IObservable.Identity
        {
            get { return Identity; }
        }

        With.Component.IValueDescription With.Component.IObservable.Description
        {
            get { return Description; }
        }

        public Identity Identity { get; set; }
        public ValueDescription Description { get; set; }
    }
}
