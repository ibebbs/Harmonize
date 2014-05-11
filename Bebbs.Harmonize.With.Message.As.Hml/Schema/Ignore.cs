
namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class Ignore : Message, With.Message.IIgnore
    {
        With.Component.IIdentity IIgnore.Entity
        {
            get { return Entity; }
        }

        With.Component.IIdentity IIgnore.Observable
        {
            get { return Observable; }
        }

        With.Component.IIdentity IIgnore.Observer
        {
            get { return Observer; }
        }

        public Identity Entity { get; set; }
        public Identity Observable { get; set; }
        public Identity Observer { get; set; }
    }
}
