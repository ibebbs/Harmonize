using Bebbs.Harmonize.With.Owl.Intuition.Values;
using System.Net.NetworkInformation;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.State.Context
{
    public interface IRegistration : IContext
    {
        Command.Endpoint.IInstance CommandEndpoint { get; }
        Version Version { get; }
        string Name { get; }
        string Remarks { get; }
        PhysicalAddress MacAddress { get; }
    }

    internal class Registration : IRegistration
    {
        public Registration(Command.Endpoint.IInstance commandEndpoint, Version version, string name, string remarks, PhysicalAddress macAddress)
        {
            CommandEndpoint = commandEndpoint;
            Version = version;
            Name = name;
            Remarks = remarks;
            MacAddress = macAddress;
        }

        public Command.Endpoint.IInstance CommandEndpoint { get; private set; }
        public Version Version { get; private set; }
        public string Name { get; private set; }
        public string Remarks { get; private set; }
        public PhysicalAddress MacAddress { get; private set; }
    }
}
