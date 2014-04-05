using Bebbs.Harmonize.With.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.Entity
{
    internal class Instance : IEntity
    {
        private const string IdentityPattern = "Bebbs.Harmonize.With.Owl.Intuition.Gateway.Entity-{0}";

        public Instance(PhysicalAddress macAddress, IEntityDescription description, IEnumerable<IObservable> observables, IEnumerable<IActionable> actionables)
        {
            Identity = new StringIdentity(string.Format(IdentityPattern, macAddress));
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
