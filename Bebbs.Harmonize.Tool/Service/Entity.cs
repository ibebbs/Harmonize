using Bebbs.Harmonize.With.Component;
using System.Collections.Generic;
using System.Linq;

namespace Bebbs.Harmonize.Tool.Service
{
    internal class Entity : IEntity
    {
        public Entity(string identity)
        {
            Identity = new Harmonize.With.Component.Identity(string.Format("Bebbs.Harmonize.Tool.Service.{0}",identity));
            Description = new EntityDescription();
            Observables = Enumerable.Empty<IObservable>();
            Actionables = Enumerable.Empty<IActionable>();
        }

        public IIdentity Identity { get; private set; }

        public IEntityDescription Description { get; private set; }

        public IEnumerable<IObservable> Observables { get; private set; }

        public IEnumerable<IActionable> Actionables { get; private set; }
    }
}
