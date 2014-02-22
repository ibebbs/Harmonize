using System.Net;

namespace Bebbs.Harmonize.With.Owl.Intuition.Command.Request
{
    public class SetUdpPushPort : IRequest
    {
        private readonly IPEndPoint _endpoint;

        public SetUdpPushPort(IPEndPoint endpoint)
        {
            _endpoint = endpoint;
        }

        public string AsString()
        {
            return string.Format("{0},{1},{2},{3},{4}", Verb.Set, Subject.Udp, string.Empty, _endpoint.Address.ToString(), _endpoint.Port.ToString());
        }
    }
}
