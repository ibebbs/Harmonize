using System;
using System.Collections.Generic;
using System.Linq;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.Entity.Observable
{
    public interface IAbstractFactory
    {
        IEnumerable<IGatewayObservable> CreateForEntityDeviceType(Entity.IInstance instance, string deviceType);
    }

    internal class AbstractFactory : IAbstractFactory
    {
        private IEnumerable<IFactory> _factories;

        public AbstractFactory(IEnumerable<IFactory> factories)
        {
            _factories = (factories ?? Enumerable.Empty<IFactory>()).ToArray();
        }

        public IEnumerable<IGatewayObservable> CreateForEntityDeviceType(Entity.IInstance instance, string deviceType)
        {
            return _factories.Where(factory => string.Equals(factory.DeviceType, deviceType, StringComparison.InvariantCultureIgnoreCase))
                             .Select(factory => factory.ForEntity(instance))
                             .ToArray();
        }
    }
}
