﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bebbs.Harmonize.With.Owl.Intuition.Configuration
{
    public interface IDevice : Gateway.Settings.IProvider
    {

    }

    public class Device : IDevice
    {
        private Lazy<Gateway.Settings.IValues> _values;

        public Device()
        {
            _values = new Lazy<Gateway.Settings.IValues>(ConstructValues);
        }

        private Gateway.Settings.IValues ConstructValues()
        {
            IPAddress ipAddress = string.IsNullOrWhiteSpace(LocalIpAddress)
              ? Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)
              : IPAddress.Parse(LocalIpAddress);

            return new Gateway.Settings.Values(
                Name,
                Remarks,
                new IPEndPoint(ipAddress, LocalCommandPort),
                new IPEndPoint(ipAddress, LocalPacketPort),
                OwlCommandKey,
                PhysicalAddress.Parse(OwlMacAddress),
                new IPEndPoint(IPAddress.Parse(OwlIpAddress), OwlCommandPort),
                TimeSpan.Parse(OwlCommandResponseTimeout),
                AutoConfigurePacketPort
            );
        }

        public IEnumerable<string> ValidateValues()
        {
            IPAddress ipAddress;
            TimeSpan timeSpan;
            PhysicalAddress physicalAddress;

            if (LocalCommandPort < 1000) yield return "LocalCommandPort must be greater than 1000";
            if (LocalPacketPort < 1000) yield return "LocalPacketPort must be greater than 1000";
            if (string.IsNullOrWhiteSpace(OwlCommandKey)) yield return "OwlCommandKey must not be specified";
            if (!Try(value => PhysicalAddress.Parse(value), OwlMacAddress, out physicalAddress)) yield return "OwlMacAddress must be specified in the format XX-XX-XX-XX-XX-XX";
            if (!IPAddress.TryParse(OwlIpAddress, out ipAddress)) yield return "OwlIpAddress must be a valid IP Address";
            if (OwlCommandPort < 1000) yield return "OwlCommandPort must be greater than 1000";
            if (!TimeSpan.TryParse(OwlCommandResponseTimeout, out timeSpan)) yield return "OwlCommandResponseTimeout must be specified in the format hh:mm:ss";
        }

        private bool Try<TSource,TTarget>(Func<TSource, TTarget> func, TSource source, out TTarget target)
        {
            try
            {
                target = func(source);
                return true;
            }
            catch (Exception)
            {
                target = default(TTarget);
                return false;
            }
        }

        Gateway.Settings.IValues Gateway.Settings.IProvider.GetValues()
        {
            return _values.Value;
        }

        /// <summary>
        /// Gets the user defined name for the gateway
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the user defined description for the gateway
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// Gets the ip address used to send commands and receive packets
        /// </summary>
        public string LocalIpAddress { get; set; }

        /// <summary>
        /// Gets the port from which to issue commands to the network owl
        /// </summary>
        public int LocalCommandPort { get; set; }

        /// <summary>
        /// Gets the local port on which we want to receive measurement packets
        /// </summary>
        public int LocalPacketPort { get; set; }

        /// <summary>
        /// Gets the "UDP key" value used to authenticate the client app with the Network OWL
        /// </summary>
        public string OwlCommandKey { get; set; }

        /// <summary>
        /// Gets the mac address of the Network Owl
        /// </summary>
        public string OwlMacAddress { get; set; }

        /// <summary>
        /// Gets the IP address of the Network Owl
        /// </summary>
        /// <remarks>
        /// This is usually UDP port 5100
        /// </remarks>
        public string OwlIpAddress { get; set; }

        /// <summary>
        /// Gets the port used by the Network Owl
        /// </summary>
        /// <remarks>
        /// This is usually UDP port 5100
        /// </remarks>
        public int OwlCommandPort { get; set; }

        /// <summary>
        /// Gets the maximum time to wait for a command response
        /// </summary>
        public string OwlCommandResponseTimeout { get; set; }

        /// <summary>
        /// Gets whether the client should automatically configure the Network OWL to transmit measurement packets to the local packet endpoint port
        /// </summary>
        public bool AutoConfigurePacketPort { get; set; }
    }
}
