using Bebbs.Harmonize.With.Component;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.Entity
{
    public interface IFactory
    {
        IInstance Create(string name, string remarks, PhysicalAddress macAddress, IEnumerable<Command.Response.Device> roster);
    }

    internal class Factory : IFactory
    {
        private readonly Observable.IAbstractFactory _observableFactory;

        public Factory(Observable.IAbstractFactory observableFactory)
        {
            _observableFactory = observableFactory;
        }

        private IEnumerable<IGatewayObservable> CreateObservable(IInstance entity, Command.Response.Device device)
        {
            return _observableFactory.CreateForEntityDeviceType(entity, device.DeviceType);
        }

        private IEnumerable<IGatewayActionable> CreateActionable(IInstance entity, Command.Response.Device device)
        {
            return Enumerable.Empty<IGatewayActionable>();
        }

        public IInstance Create(string name, string remarks, PhysicalAddress macAddress, IEnumerable<Command.Response.Device> roster)
        {
            Instance instance = new Instance(macAddress, name, remarks);

            instance.Observables = roster.SelectMany(device => CreateObservable(instance, device));
            instance.Actionables = roster.SelectMany(device => CreateActionable(instance, device));

            return instance;
        }
    }
}
