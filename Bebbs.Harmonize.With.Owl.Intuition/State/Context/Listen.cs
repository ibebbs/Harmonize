using Bebbs.Harmonize.With.Owl.Intuition.Values;
using System.Net;

namespace Bebbs.Harmonize.With.Owl.Intuition.State.Context
{
    public interface IListen : IContext
    {
        Command.Endpoint.IInstance CommandEndpoint { get; }
        Version Version { get; }
        IPEndPoint LocalPacketEndpoint { get; }
    }

    internal class Listen : IListen
    {
        public Listen(Command.Endpoint.IInstance commandEndpoint, Values.Version version, IPEndPoint localPacketEndpoint)
        {
            CommandEndpoint = commandEndpoint;
            Version = version;
            LocalPacketEndpoint = localPacketEndpoint;
        }

        public Command.Endpoint.IInstance CommandEndpoint { get; private set; }
        public Version Version { get; private set; }
        public IPEndPoint LocalPacketEndpoint { get; private set; }
    }
}
