
using System;
using System.Net;
using System.Net.NetworkInformation;
namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.Settings
{
    public interface IValues
    {
        /// <summary>
        /// Gets the user defined name for the gateway
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the user defined description for the gateway
        /// </summary>
        string Remarks { get; }

        /// <summary>
        /// Gets the <see cref="IPEndpoint"/> of the local port to send command messages from
        /// </summary>
        IPEndPoint LocalCommandEndpoint { get; }

        /// <summary>
        /// Gets the <see cref="IPEndpoint"/> of the local port on which we want to receive measurement packets
        /// </summary>
        IPEndPoint LocalPacketEndpoint { get; }

        /// <summary>
        /// Gets the "UDP key" value used to authenticate the client app with the Network OWL
        /// </summary>
        string OwlCommandKey { get; }

        /// <summary>
        /// Gets the <see cref="PhysicalAddress"/> of the Network Owl
        /// </summary>
        PhysicalAddress OwlMacAddress { get; }

        /// <summary>
        /// Gets the <see cref="IPEndpoint" of the Network Owl command port/>
        /// </summary>
        /// <remarks>
        /// This is usually UDP port 5100
        /// </remarks>
        IPEndPoint OwlCommandEndpoint { get; }

        /// <summary>
        /// Gets the maximum time to wait for a command response
        /// </summary>
        TimeSpan OwlCommandResponseTimeout { get; }

        /// <summary>
        /// Gets whether the client should automatically configure the Network OWL to transmit measurement packets to the local packet endpoint port
        /// </summary>
        bool AutoConfigurePacketPort { get; }
    }

    internal class Values : IValues
    {
        public Values(string name, string remarks, IPEndPoint localCommandEndpoint, IPEndPoint localPacketEndpoint, string owlCommandKey, PhysicalAddress owlMacAddress, IPEndPoint owlCommandEndpoint, TimeSpan owlCommandResponseTimeout, bool autoConfigurePacketPort)
        {
            Name = name;
            Remarks = remarks;
            LocalCommandEndpoint = localCommandEndpoint;
            LocalPacketEndpoint = localPacketEndpoint;
            OwlCommandKey = owlCommandKey;
            OwlMacAddress = owlMacAddress;
            OwlCommandEndpoint = owlCommandEndpoint;
            OwlCommandResponseTimeout = owlCommandResponseTimeout;
            AutoConfigurePacketPort = autoConfigurePacketPort;
        }

        public string Name { get; private set; }
        public string Remarks { get; private set; }
        public IPEndPoint LocalCommandEndpoint { get; private set; }
        public IPEndPoint LocalPacketEndpoint { get; private set; }
        public string OwlCommandKey { get; private set; }
        public PhysicalAddress OwlMacAddress { get; private set; }
        public IPEndPoint OwlCommandEndpoint { get; private set; }
        public TimeSpan OwlCommandResponseTimeout { get; private set; }
        public bool AutoConfigurePacketPort { get; private set; }
    }
}
