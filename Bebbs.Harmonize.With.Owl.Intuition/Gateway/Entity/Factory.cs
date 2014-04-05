using Bebbs.Harmonize.With.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.Entity
{
    public interface IFactory
    {
        IEntity Create(string name, string remarks, PhysicalAddress macAddress, IEnumerable<Command.Response.Device> roster);
    }

    internal class Factory : IFactory
    {
        private IEnumerable<IObservable> CreateObservable(Command.Response.Device device)
        {
            return Enumerable.Empty<IObservable>();
        }

        private IEnumerable<IActionable> CreateActionable(Command.Response.Device device)
        {
            return Enumerable.Empty<IActionable>();
        }

        public IEntity Create(string name, string remarks, PhysicalAddress macAddress, IEnumerable<Command.Response.Device> roster)
        {
            Description description = new Description(name, remarks);
            IEnumerable<IObservable> observables = roster.SelectMany(CreateObservable);
            IEnumerable<IActionable> actionables = roster.SelectMany(CreateActionable);

            return new Instance(macAddress, description, observables, actionables);
        }
    }
}
