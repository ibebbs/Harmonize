
using System.Net;
namespace Bebbs.Harmonize.With.Owl.Intuition.Command.Response
{
    public class Udp : IResponse
    {
        public Udp(Status status, string hostName, string ipAddress, int port)
        {
            Status = status;
            HostName = hostName;

            Endpoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
        }

        public Status Status { get; private set; }
        public string HostName { get; private set; }
        public IPEndPoint Endpoint { get; private set; }
    }
}
