using Bebbs.Harmonize.With.Owl.Intuition.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bebbs.Harmonize.With.Owl.Intuition.Configuration
{
    [XmlRoot("Gateway")]
    public class Gateway : Settings.IProvider
    {
        private Lazy<IValues> _values;

        public Gateway()
        {
            _values = new Lazy<IValues>(ConstructValues);
        }

        private Settings.IValues ConstructValues()
        {
            IPAddress ipAddress = IPAddress.Parse(LocalIpAddress);

            return new Settings.Values(
                new IPEndPoint(ipAddress, LocalCommandPort),
                new IPEndPoint(ipAddress, LocalPacketPort),
                OwlCommandKey,
                new IPEndPoint(IPAddress.Parse(OwlIpAddress), OwlCommandPort),
                TimeSpan.Parse(OwlCommandResponseTimeout),
                AutoConfigurePacketPort
            );
        }

        public IEnumerable<string> ValidateValues()
        {
            IPAddress ipAddress;
            TimeSpan timeSpan;

            if (!IPAddress.TryParse(LocalIpAddress, out ipAddress)) yield return "LocalIpAddress must be a valid IP Address";
            if (LocalCommandPort < 1000) yield return "LocalCommandPort must be greater than 1000";
            if (LocalPacketPort < 1000) yield return "LocalPacketPort must be greater than 1000";
            if (string.IsNullOrWhiteSpace(OwlCommandKey)) yield return "OwlCommandKey must not be specified";
            if (!IPAddress.TryParse(OwlIpAddress, out ipAddress)) yield return "OwlIpAddress must be a valid IP Address";
            if (OwlCommandPort < 1000) yield return "OwlCommandPort must be greater than 1000";
            if (!TimeSpan.TryParse(OwlCommandResponseTimeout, out timeSpan)) yield return "OwlCommandResponseTimeout must be specified in the format hh:mm:ss";
        }

        public Settings.IValues GetValues()
        {
            return _values.Value;
        }

        /// <summary>
        /// Gets the ip address used to send commands and receive packets
        /// </summary>
        [XmlAttribute("localIpAddress")]
        public string LocalIpAddress { get; set; }

        /// <summary>
        /// Gets the port from which to issue commands to the network owl
        /// </summary>
        [XmlAttribute("localCommandPort")]
        public int LocalCommandPort { get; set; }

        /// <summary>
        /// Gets the local port on which we want to receive measurement packets
        /// </summary>
        [XmlAttribute("localPacketPort")]
        public int LocalPacketPort { get; set; }

        /// <summary>
        /// Gets the "UDP key" value used to authenticate the client app with the Network OWL
        /// </summary>
        [XmlAttribute("owlCommandKey")]
        public string OwlCommandKey { get; set; }

        /// <summary>
        /// Gets the IP address of the Network Owl
        /// </summary>
        /// <remarks>
        /// This is usually UDP port 5100
        /// </remarks>
        [XmlAttribute("owlIpAddress")]
        public string OwlIpAddress { get; set; }

        /// <summary>
        /// Gets the port used by the Network Owl
        /// </summary>
        /// <remarks>
        /// This is usually UDP port 5100
        /// </remarks>
        [XmlAttribute("owlCommandPort")]
        public int OwlCommandPort { get; set; }

        /// <summary>
        /// Gets the maximum time to wait for a command response
        /// </summary>
        [XmlAttribute("owlCommandResponseTimeout")]
        public string OwlCommandResponseTimeout { get; set; }

        /// <summary>
        /// Gets whether the client should automatically configure the Network OWL to transmit measurement packets to the local packet endpoint port
        /// </summary>
        [XmlAttribute("autoConfigurePacketPort")]
        public bool AutoConfigurePacketPort { get; set; }
    }
}
