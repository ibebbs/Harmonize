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

        private UdpClient _udpClient;
        private IConnectableObservable<IResponse> _responses;

        private IDisposable _responseSubscription;

        public Instance(Response.IParser responseParser, IPEndPoint localEndpoint, IPEndPoint remoteEndpoint, TimeSpan requestTimeout)
        {
            _localEndpoint = localEndpoint;
            _remoteEndpoint = remoteEndpoint;
            _requestTimeout = requestTimeout;

            _udpClient = new UdpClient(_localEndpoint);
            _responses = Observable.FromAsync(_udpClient.ReceiveAsync).Repeat()
                                   .Select(result => result.Buffer)
                                   .Select(Encoding.ASCII.GetString)
                                   .SelectMany(responseParser.GetResponses)
                                   .Publish();
        }

        public void Dispose()
        {
            Close();

            if (_udpClient != null)
            {
                _responses = null;

                _udpClient.Close();
                _udpClient = null;
            }
        }

        private bool RequestFailed<T>(T response) where T : IResponse
        {
            return response.Status != Status.Ok;
        }

        private Exception RequestException(IRequest request, IResponse response)
        {
            return new InvalidOperationException(string.Format("Received error in response to the following request: '{0}'", request.AsString()));
        }

        private Task<T> Send<T>(IRequest request) where T : IResponse
        {
            byte[] datagram = Encoding.ASCII.GetBytes(request.AsString());

            Task<T> result = _responses.OfType<T>().ThrowWhen<T>(RequestFailed, response => RequestException(request, response)).Timeout(_requestTimeout).ToTask();

            _udpClient.SendAsync(datagram, datagram.Length, _remoteEndpoint);

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
