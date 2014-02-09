using System.Collections.Generic;

namespace Bebbs.Harmonize.With.Component
{
    public interface IEntity
    {
        IIdentity Identity { get; }
        IEntityDescription Description { get; }
        IEnumerable<IObservable> Observables { get; }
        IEnumerable<IActionable> Actionables { get; }
    }
}
