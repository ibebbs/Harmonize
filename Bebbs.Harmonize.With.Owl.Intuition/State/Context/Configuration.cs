using Bebbs.Harmonize.With.Owl.Intuition.Values;
using System.Net;

namespace Bebbs.Harmonize.With.Owl.Intuition.State.Context
{
    public interface IConfiguration : IContext
    {
        Command.Endpoint.IInstance CommandEndpoint { get; }
        Version Version { get; }
        bool AutoConfigurePacketEndpoint { get; }
        IPEndPoint LocalPacketEndpoint { get; }
    }

    internal class Configuration : IConfiguration
    {
        public Configuration(Command.Endpoint.IInstance commandEndpoint, Version version, bool autoConfigurePacketEndpoint, IPEndPoint localPacketEndpoint)
        {
            CommandEndpoint = commandEndpoint;
            Version = version;
            AutoConfigurePacketEndpoint = autoConfigurePacketEndpoint;
            LocalPacketEndpoint = localPacketEndpoint;
        }

        public Command.Endpoint.IInstance CommandEndpoint { get; private set; }
        public Version Version { get; private set; }
        public bool AutoConfigurePacketEndpoint { get; private set; }
        public IPEndPoint LocalPacketEndpoint { get; private set; }
    }
}
