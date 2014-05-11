using EventStore.ClientAPI;
using System;
using System.Linq;
using System.Net;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Store.Using.EventStore
{
    public class Connector : Host.IService
    {
        private static With.Component.IIdentity Identity = new With.Component.Identity("Bebbs.Harmonize.With.Store.Using.EventStore.Connector");

        private readonly Configuration.ISettings _configurationSettings;
        private readonly Event.IProvider _eventProvider;
        private readonly Messaging.Component.IEndpoint _endpoint;
        private readonly Subject<Message.IMessage> _messages;

        private IEventStoreConnection _connection;
        private IDisposable _subscription;

        public Connector(Configuration.ISettings configurationSettings, Event.IProvider eventProvider, Messaging.Component.IEndpoint endpoint)
        {
            _configurationSettings = configurationSettings;
            _eventProvider = eventProvider;
            _endpoint = endpoint;

            _messages = new Subject<Message.IMessage>();
        }

        private async void Append(EventData eventData)
        {
            if (_connection != null)
            {
                await _connection.AppendToStreamAsync(_configurationSettings.Stream, ExpectedVersion.Any, eventData);
            }
        }

        public void Initialize()
        {
            _endpoint.Initialize();

            _subscription = new CompositeDisposable(
                _messages.OfType<With.Message.IRegister>().Do(Instrumentation.Store.Storing).Select(_eventProvider.ToEvent).Subscribe(Append),
                _messages.OfType<With.Message.IDeregister>().Do(Instrumentation.Store.Storing).Select(_eventProvider.ToEvent).Subscribe(Append),
                _messages.OfType<With.Message.IObservation>().Do(Instrumentation.Store.Storing).Select(_eventProvider.ToEvent).Subscribe(Append),
                _messages.OfType<With.Message.IAction>().Do(Instrumentation.Store.Storing).Select(_eventProvider.ToEvent).Subscribe(Append)
            );
        }

        public async Task Start()
        {
            ConnectionSettings connectionSettings = ConnectionSettings.Create();
            connectionSettings.Connected = (c, e) => Instrumentation.Store.ConnectedToEventStore(e.Address, e.Port);
            connectionSettings.Disconnected = (c, e) => Instrumentation.Store.DisconnectedFromEventStore(e.Address, e.Port);
            connectionSettings.ErrorOccurred = (c, e) => Instrumentation.Store.EventStoreErrorOccured(e);
            connectionSettings.Reconnecting = (c) => Instrumentation.Store.ReconnectingToEventStore();
            connectionSettings.AuthenticationFailed = (c, s) => Instrumentation.Store.EventStoreAuthenticationFailed(s);

            IEventStoreConnection connection = EventStoreConnection.Create(connectionSettings, new IPEndPoint(_configurationSettings.Host, _configurationSettings.Port), "Harmonize");

            Instrumentation.Store.ConnectingToEventStore(_configurationSettings.Host.ToString(), _configurationSettings.Port);

            await connection.ConnectAsync();

            _connection = connection;

            _endpoint.Add(Identity, _configurationSettings.Component, _messages);
        }

        public Task Stop()
        {
            _endpoint.Remove(Identity, _configurationSettings.Component.Identity);

            Instrumentation.Store.DisconnectingFromEventStore(_configurationSettings.Host.ToString(), _configurationSettings.Port);

            IEventStoreConnection connection = _connection;
            _connection = null;

            connection.Close();
            connection.Dispose();

            return Task.FromResult<object>(null);
        }

        public void Cleanup()
        {
            if (_subscription != null)
            {
                _subscription.Dispose();
                _subscription = null;
            }

            _endpoint.Cleanup();
        }
    }
}
