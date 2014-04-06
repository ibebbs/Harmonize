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
        IObservable<IReading> Readings { get; }

        void Open();
        void Close();
    }

    internal class Instance : IInstance
    {
        private readonly IParser _packetParser;
        private readonly IPEndPoint _localPacketEndpoint;

        private UdpClient _udpClient;
        private IConnectableObservable<IReading> _readings;
        private IDisposable _subscription;

        public Instance(IParser packetParser, IPEndPoint localPacketEndpoint)
        {
            _packetParser = packetParser;
            _localPacketEndpoint = localPacketEndpoint;

            _udpClient = new UdpClient();
            _udpClient.Client.Bind(_localPacketEndpoint);

            _readings = Observable.FromAsync(_udpClient.ReceiveAsync).Repeat()
                                  .Select(result => result.Buffer)
                                  .Select(Encoding.ASCII.GetString)
                                  .Do(Instrumentation.Packet.Endpoint.Receive)
                                  .SelectMany(_packetParser.GetReadings)
                                  .Do(Instrumentation.Packet.Endpoint.Reading)
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

        public IObservable<IReading> Readings 
        {
            get { return _readings; }
        }

        public void Open()
        {
            _subscription = _readings.Connect();
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
