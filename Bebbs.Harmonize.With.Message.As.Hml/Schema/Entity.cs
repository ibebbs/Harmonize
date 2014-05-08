using System.Collections.Generic;

namespace Bebbs.Harmonize.With.Message.As.Hml.Schema
{
    public class Entity : With.Component.IEntity
    {
        With.Component.IIdentity With.Component.IEntity.Identity
        {
            get { return Identity; }
        }

        With.Component.IEntityDescription With.Component.IEntity.Description
        {
            get { return Description; }
        }

        IEnumerable<With.Component.IObservable> With.Component.IEntity.Observables
        {
            get { return Observables; }
        }

        IEnumerable<With.Component.IActionable> With.Component.IEntity.Actionables
        {
            get { return Actionables; }
        }

        public Identity Identity { get; set; }
        public EntityDescription Description { get; set; }
        public Observable[] Observables { get; set; }
        public Actionable[] Actionables { get; set; }

    }
}
