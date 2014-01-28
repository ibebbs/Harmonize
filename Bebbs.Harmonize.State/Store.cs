using Bebbs.Harmonize.With;
using EventStore.ClientAPI;
using System;
using System.Net;
using System.Reactive.Disposables;

namespace Bebbs.Harmonize.State
{
    public interface IStore : IInitializeAtStartup, ICleanupAtShutdown
    {
    }

    internal class Store : IStore
    {
        public static readonly string StreamName = "Bebbs-Harmonize-State";

        // TODO: Should be moved to configuration
        private const string IpAddress = "127.0.0.1";
        private const int Port = 2113;

        private readonly IGlobalEventAggregator _eventAggregator;
        private readonly Event.ITranslator _eventTranslator;

        private IEventStoreConnection _connection;
        private IDisposable _subscription;

        public Store(IGlobalEventAggregator eventAggregator, Event.ITranslator eventTranslator)
        {
            _eventAggregator = eventAggregator;
            _eventTranslator = eventTranslator;
        }

        private async void Process(With.Message.IObservation message)
        {
            Instrumentation.Store.Storing(message);

            EventData ed = _eventTranslator.Translate(message);

            await _connection.AppendToStreamAsync(StreamName, ExpectedVersion.Any, ed);
        }

        private async void Process(With.Message.IStopped message)
        {
            Instrumentation.Store.Storing(message);

            EventData ed = _eventTranslator.Translate(message);

            await _connection.AppendToStreamAsync(StreamName, ExpectedVersion.Any, ed);
        }

        private async void Process(With.Message.IStarted message)
        {
            Instrumentation.Store.Storing(message);

            EventData ed = _eventTranslator.Translate(message);

            await _connection.AppendToStreamAsync(StreamName, ExpectedVersion.Any, ed);
        }

        private async void Process(With.Message.IDeregisterDevice message)
        {
            Instrumentation.Store.Storing(message);

            EventData ed = _eventTranslator.Translate(message);

            await _connection.AppendToStreamAsync(StreamName, ExpectedVersion.Any, ed);
        }

        private async void Process(With.Message.IRegisterDevice message)
        {
            Instrumentation.Store.Storing(message);

            EventData ed = _eventTranslator.Translate(message);

            await _connection.AppendToStreamAsync(StreamName, ExpectedVersion.Any, ed);
        }

        public void Initialize()
        {
            _connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Parse(IpAddress), Port));

            Instrumentation.Store.ConnectingToEventStore(IpAddress, Port);

            _connection.Connect();

            _subscription = new CompositeDisposable(
                _eventAggregator.GetEvent<With.Message.IRegisterDevice>().Subscribe(Process),
                _eventAggregator.GetEvent<With.Message.IDeregisterDevice>().Subscribe(Process),
                _eventAggregator.GetEvent<With.Message.IStarted>().Subscribe(Process),
                _eventAggregator.GetEvent<With.Message.IStopped>().Subscribe(Process),
                _eventAggregator.GetEvent<With.Message.IObservation>().Subscribe(Process),
                Disposable.Create(() => Instrumentation.Store.DisconnectingFromEventStore(IpAddress, Port))
            );
        }

        public void Cleanup()
        {
            if (_subscription != null)
            {
                _subscription.Dispose();
                _subscription = null;
            }

            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
