using Bebbs.Harmonize.With;
using EventStore.ClientAPI;
using System;
using System.Net;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.State
{
    public interface IStore : IInitialize, IStart, IStop, ICleanup
    {
    }

    internal class Store : IStore
    {
        public static readonly string StreamName = "Bebbs-Harmonize-State";

        // TODO: Should be moved to configuration
        private const string IpAddress = "192.168.1.22";
        private const int Port = 1113;

        private readonly IGlobalEventAggregator _eventAggregator;
        private readonly Event.ITranslator _eventTranslator;

        private IEventStoreConnection _connection;
        private IDisposable _subscription;

        public Store(IGlobalEventAggregator eventAggregator, Event.ITranslator eventTranslator)
        {
            _eventAggregator = eventAggregator;
            _eventTranslator = eventTranslator;
        }

        private async void Append(EventData eventData)
        {
            if (_connection != null)
            {
                await _connection.AppendToStreamAsync(StreamName, ExpectedVersion.Any, eventData);
            }
        }

        public void Initialize()
        {
            _subscription = new CompositeDisposable(
                _eventAggregator.GetEvent<With.Message.IRegister>().Do(Instrumentation.Store.Storing).Select(_eventTranslator.Translate).Subscribe(Append),
                _eventAggregator.GetEvent<With.Message.IDeregister>().Do(Instrumentation.Store.Storing).Select(_eventTranslator.Translate).Subscribe(Append),
                _eventAggregator.GetEvent<With.Message.IStarted>().Do(Instrumentation.Store.Storing).Select(_eventTranslator.Translate).Subscribe(Append),
                _eventAggregator.GetEvent<With.Message.IStopped>().Do(Instrumentation.Store.Storing).Select(_eventTranslator.Translate).Subscribe(Append),
                _eventAggregator.GetEvent<With.Message.IObservation>().Do(Instrumentation.Store.Storing).Select(_eventTranslator.Translate).Subscribe(Append)
            );
        }

        public async Task Start()
        {
            if (_connection != null)
            {
                throw new InvalidOperationException("Cannot start the Store as it has already been started");
            }

            ConnectionSettings connectionSettings = ConnectionSettings.Create();
            connectionSettings.Connected = (c, e) => Instrumentation.Store.ConnectedToEventStore(e.Address, e.Port);
            connectionSettings.Disconnected = (c, e) => Instrumentation.Store.DisconnectedFromEventStore(e.Address, e.Port);
            connectionSettings.ErrorOccurred = (c, e) => Instrumentation.Store.EventStoreErrorOccured(e);
            connectionSettings.Reconnecting = (c) => Instrumentation.Store.ReconnectingToEventStore();
            connectionSettings.AuthenticationFailed = (c, s) => Instrumentation.Store.EventStoreAuthenticationFailed(s);

            IEventStoreConnection connection = EventStoreConnection.Create(connectionSettings, new IPEndPoint(IPAddress.Parse(IpAddress), Port), "Harmonize");

            Instrumentation.Store.ConnectingToEventStore(IpAddress, Port);

            await connection.ConnectAsync();

            _connection = connection;
        }

        public Task Stop()
        {
            if (_connection == null)
            {
                throw new InvalidOperationException("Cannot stop the Store as it has already been stopped");
            }

            Instrumentation.Store.DisconnectingFromEventStore(IpAddress, Port);

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
        }
    }
}
