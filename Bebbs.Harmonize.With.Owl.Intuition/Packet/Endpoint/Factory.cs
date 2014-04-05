using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Owl.Intuition.Packet.Endpoint
{
    public interface IFactory
    {
        IInstance CreateEndpoint();
    }

    internal class Factory : IFactory
    {
        private readonly Gateway.Settings.IProvider _settingsProvider;
        private readonly IParser _packetParser;

        public Factory(Gateway.Settings.IProvider settingsProvider, IParser packetParser)
        {
            _settingsProvider = settingsProvider;
            _packetParser = packetParser;
        }

        public IInstance CreateEndpoint()
        {
            Gateway.Settings.IValues settings = _settingsProvider.GetValues();

            return new Instance(_packetParser, settings.LocalPacketEndpoint);
        }
    }
}
