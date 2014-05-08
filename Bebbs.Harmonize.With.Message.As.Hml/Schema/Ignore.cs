
namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class Ignore : Message, With.Message.IIgnore
    {
        Component.IIdentity IIgnore.Entity
        {
            get { return Entity; }
        }

        Component.IIdentity IIgnore.Observable
        {
            get { return Observable; }
        }

        Component.IIdentity IIgnore.Observer
        {
            get { return Observer; }
        }

        public Identity Entity { get; set; }
        public Identity Observable { get; set; }
        public Identity Observer { get; set; }
    }
}
