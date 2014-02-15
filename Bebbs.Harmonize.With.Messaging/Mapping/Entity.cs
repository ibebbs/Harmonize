using System.Collections.Generic;

namespace Bebbs.Harmonize.With.Messaging.Mapping
{
    internal class Entity : Component.IEntity
    {
        public Component.IIdentity Identity { get; set; }
        public Component.IEntityDescription Description { get; set; }
        public IEnumerable<Component.IObservable> Observables { get; set; }
        public IEnumerable<Component.IActionable> Actionables { get; set; }
    }
}
