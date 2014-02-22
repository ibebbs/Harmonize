using System;
using System.Net;
using System.Net.Sockets;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace Bebbs.Harmonize.With.Owl.Intuition.Packet.Endpoint
{
    public interface IInstance : IDisposable
    {
        IObservable<IPacket> Packets { get; }

        void Open();
        void Close();
    }

    internal class Instance : IInstance
    {
        private readonly IParser _packetParser;
        private readonly IPEndPoint _localPacketEndpoint;

        private UdpClient _udpClient;
        private IConnectableObservable<IPacket> _packets;
        private IDisposable _subscription;

        public Instance(IParser packetParser, IPEndPoint localPacketEndpoint)
        {
            _packetParser = packetParser;
            _localPacketEndpoint = localPacketEndpoint;

            _udpClient = new UdpClient(_localPacketEndpoint);

            _packets = Observable.FromAsync(_udpClient.ReceiveAsync).Repeat()
                                 .Select(result => result.Buffer)
                                 .Select(Encoding.ASCII.GetString)
                                 .SelectMany(_packetParser.GetPackets)
                                 .Publish();
        }

        public void Dispose()
        {
            Close();

            if (_udpClient != null)
            {
                _udpClient.Close();
                _udpClient = null;
            }
        }

        public IObservable<IPacket> Packets 
        {
            get { return _packets; }
        }

        public void Open()
        {
            _subscription = _packets.Connect();
        }

        public void Close()
        {
            if (_subscription != null)
            {
                _subscription.Dispose();
                _subscription = null;
            }
        }
    }
}
