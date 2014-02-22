using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bebbs.Harmonize.With.Text;

namespace Bebbs.Harmonize.With.Owl.Intuition.Command.Response.Builder
{
    internal class UdpResponse : IBuilder
    {
        private const string StatusGroup = "Status";
        private const string HostNameGroup = "HostName";
        private const string IpAddressGroup = "IpAddress";
        private const string PortGroup = "Port";
        private const string UdpResponseName = "UdpResponse";
        private const string UdpResponsePattern = @"(?<Status>OK|ERROR),UDP,(?<HostName>\w*),(?<IpAddress>\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}),(?<Port>\d+)";

        public IResponse Build(System.Text.RegularExpressions.Match match)
        {
            Status status = match.ReadGroupAs<Status>(StatusGroup, value =>(Status)Enum.Parse(typeof(Status), value));
            string hostName = match.ReadGroupValue(HostNameGroup);
            string ipAddress = match.ReadGroupValue(IpAddressGroup);
            int port = match.ReadGroupAs<int>(PortGroup, Int32.Parse);

            return new Command.Response.Udp(status, hostName, ipAddress, port);
        }

        public string Name
        {
            get { return UdpResponseName; }
        }

        public string Regex
        {
            get { return UdpResponsePattern; }
        }
    }
}
