using System.Collections.Generic;
using System.Linq;

namespace Bebbs.Harmonize.With.Component
{
    public interface IEntity
    {
        IIdentity Identity { get; }
        IEntityDescription Description { get; }
        IEnumerable<IObservable> Observables { get; }
        IEnumerable<IActionable> Actionables { get; }
    }

    public class Entity : IEntity
    {
        public Entity(IIdentity identity, IEntityDescription description, IEnumerable<IObservable> observables, IEnumerable<IActionable> actionables)
        {
            Identity = identity;
            Description = description;
            Observables = (observables ?? Enumerable.Empty<IObservable>()).ToArray();
            Actionables = (actionables ?? Enumerable.Empty<IActionable>()).ToArray();
        }

        public IIdentity Identity { get; private set; }
        public IEntityDescription Description { get; private set; }
        public IEnumerable<IObservable> Observables { get; private set; }
        public IEnumerable<IActionable> Actionables { get; private set; }
    }
}
