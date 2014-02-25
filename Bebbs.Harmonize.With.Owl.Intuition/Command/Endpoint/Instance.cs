using System;
using System.Net;
using System.Net.Sockets;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using Bebbs.Harmonize.With;

namespace Bebbs.Harmonize.With.Owl.Intuition.Command.Endpoint
{
    public interface IInstance : IDisposable
    {
        Task<Response.Version> Send(Request.GetVersion message);
        Task<Response.Rosta> Send(Request.GetRosta message);
        Task<Response.Device> Send(Request.GetDevice message);
        Task<Response.Udp> Send(Request.GetUpdPushPort message);
        Task<Response.Udp> Send(Request.SetUdpPushPort message);
        Task<Response.Save> Send(Request.Save message);

        void Open();
        void Close();
    }

    internal class Instance : IInstance
    {
        private readonly IPEndPoint _localEndpoint;
        private readonly IPEndPoint _remoteEndpoint;
        private readonly TimeSpan _requestTimeout;
        private readonly string _udpKey;

        private UdpClient _udpReceive;
        private UdpClient _udpSend;
        private IConnectableObservable<IResponse> _responses;

        private IDisposable _responseSubscription;

        public Instance(Response.IParser responseParser, IPEndPoint localEndpoint, IPEndPoint remoteEndpoint, TimeSpan requestTimeout, string udpKey)
        {
            _localEndpoint = localEndpoint;
            _remoteEndpoint = remoteEndpoint;
            _requestTimeout = requestTimeout;
            _udpKey = udpKey;

            _udpReceive = BuildSocket(localEndpoint);
            _udpSend = BuildSocket(localEndpoint);

            _responses = Observable.FromAsync(_udpReceive.ReceiveAsync).Repeat()
                                   .Select(result => result.Buffer)
                                   .Select(Encoding.ASCII.GetString)
                                   .Do(Instrumentation.Command.Endpoint.Receive)
                                   .SelectMany(responseParser.GetResponses)
                                   .Do(Instrumentation.Command.Endpoint.Response)
                                   .Publish();
        }

        private UdpClient BuildSocket(IPEndPoint localEndpoint)
        {
            UdpClient udpClient = new UdpClient();
            udpClient.Client.ExclusiveAddressUse = false;
            udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udpClient.Client.Bind(localEndpoint);
            return udpClient;
        }

        public void Dispose()
        {
            Close();

            if (_udpSend != null)
            {
                _udpSend.Close();
                _udpSend = null;
            }

            if (_udpReceive != null)
            {
                _responses = null;

                _udpReceive.Close();
                _udpReceive = null;
            }
        }

        private bool RequestFailed<T>(T response) where T : IResponse
        {
            return response.Status != Status.Ok;
        }

        private Exception RequestException(IRequest request, IResponse response)
        {
            Exception exception = new InvalidOperationException(string.Format("Received error in response to the following request: '{0}'", request.AsString()));

            Instrumentation.Command.Endpoint.Error(exception);

            return exception;
        }

        private string AddKey(string command)
        {
            return string.Format("{0},{1}", command, _udpKey);
        }

        private Task<T> Send<T>(IRequest request) where T : IResponse
        {
            Instrumentation.Command.Endpoint.Request(request);

            string requestString = AddKey(request.AsString());

            Instrumentation.Command.Endpoint.Send(requestString);

            byte[] datagram = Encoding.ASCII.GetBytes(requestString);

            Task<T> result = _responses.OfType<T>()
                                       .ThrowWhen<T>(RequestFailed, response => RequestException(request, response))
                                       .Timeout(_requestTimeout)
                                       .Take(1)
                                       .ToTask();

            _udpSend.Send(datagram, datagram.Length, _remoteEndpoint);

            return result;
        }

        public Task<Response.Version> Send(Request.GetVersion message)
        {
            return Send<Response.Version>(message);
        }

        public Task<Response.Rosta> Send(Request.GetRosta message)
        {
            return Send<Response.Rosta>(message);
        }

        public Task<Response.Device> Send(Request.GetDevice message)
        {
            return Send<Response.Device>(message);
        }

        public Task<Response.Udp> Send(Request.GetUpdPushPort message)
        {
            return Send<Response.Udp>(message);
        }

        public Task<Response.Udp> Send(Request.SetUdpPushPort message)
        {
            return Send<Response.Udp>(message);
        }

        public Task<Response.Save> Send(Request.Save message)
        {
            return Send<Response.Save>(message);
        }

        public void Open()
        {
            _responseSubscription = _responses.Connect();
        }

        public void Close()
        {
            if (_responseSubscription != null)
            {
                _responseSubscription.Dispose();
                _responseSubscription = null;
            }
        }
    }
}
