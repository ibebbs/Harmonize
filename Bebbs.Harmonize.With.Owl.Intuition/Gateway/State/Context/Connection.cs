using System.Net;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.State.Context
{
    public interface IConnection : IContext
    {
        IPEndPoint LocalCommandEndpoint { get; }
        IPEndPoint RemoteCommandEndpoint { get; }
    }

    internal class Connection : IConnection
    {
        public Connection(IPEndPoint localCommandEndpoint, IPEndPoint remoteCommandEndpoint)
        {
            LocalCommandEndpoint = localCommandEndpoint;
            RemoteCommandEndpoint = remoteCommandEndpoint;
        }

        public IPEndPoint LocalCommandEndpoint { get; private set; }
        public IPEndPoint RemoteCommandEndpoint { get; private set; }
    }
}
