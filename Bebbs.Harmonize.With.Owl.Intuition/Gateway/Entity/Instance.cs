using Bebbs.Harmonize.With.Component;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.Entity
{
    public interface IInstance : IEntity, IInitialize, ICleanup
    {
    }

    internal class Instance : IInstance
    {
        private const string IdentityPattern = "Bebbs.Harmonize.With.Owl.Intuition.Gateway.Entity-{0}";

        public Instance(PhysicalAddress macAddress, string name, string remarks)
        {
            Identity = new StringIdentity(string.Format(IdentityPattern, macAddress));
            Description = new EntityDescription(name, remarks);
        }

        IEnumerable<IObservable> IEntity.Observables
        {
            get { return Observables; }
        }

        IEnumerable<IActionable> IEntity.Actionables
        {
            get { return Actionables; }
        }

        public void Initialize()
        {
            Observables.ForEach(observable => observable.Initialize());
        }

        public void Cleanup()
        {
            Observables.ForEach(observable => observable.Cleanup());
        }

        public IIdentity Identity { get; private set; }

        public IEntityDescription Description { get; private set; }

        public IEnumerable<IGatewayObservable> Observables { get; set; }

        public IEnumerable<IGatewayActionable> Actionables { get; set; }
    }
}
