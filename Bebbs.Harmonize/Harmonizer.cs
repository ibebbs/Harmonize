using Bebbs.Harmonize.With;
using EventSourceProxy;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Bebbs.Harmonize
{
    [EventSourceImplementation(Name = "Bebbs-Harmonize")]
    public interface IHarmonizer : Host.IService
    {
    }

    internal class Harmonizer : IHarmonizer
    {
        private readonly With.Messaging.Service.IEndpoint _serviceEndpoint;
        private readonly Subject<With.Message.IMessage> _messages;
        private readonly IConnectableObservable<With.Message.IMessage> _messageObservable;

        private IDisposable _messageSubscription;
        private IDisposable _consumerSubscription;

        public Harmonizer(With.Messaging.Service.IEndpoint serviceEndpoint)
        {
            _serviceEndpoint = serviceEndpoint;
            _messages = new Subject<With.Message.IMessage>();

            _messageObservable = _messages.Publish();
        }

        public async Task Initialize()
        {
            await _serviceEndpoint.Initialize();

            _consumerSubscription = new CompositeDisposable(
                _messageObservable.OfType<With.Message.IRegister>().Do(Instrument.Harmonize.Action.Register).Subscribe(message => _serviceEndpoint.Register(message.Entity.Identity)),
                _messageObservable.OfType<With.Message.IDeregister>().Do(Instrument.Harmonize.Action.Deregister).Subscribe(message => _serviceEndpoint.Deregister(message.Entity)),
                _messageObservable.OfType<With.Message.IAdd>().Do(Instrument.Harmonize.Action.Add).Subscribe(message => _serviceEndpoint.Add(message.Component.Identity)),
                _messageObservable.OfType<With.Message.IRemove>().Do(Instrument.Harmonize.Action.Remove).Subscribe(message => _serviceEndpoint.Remove(message.Component)),
                _messageObservable.OfType<With.Message.IObserve>().Do(Instrument.Harmonize.Action.Observe).Subscribe(message => _serviceEndpoint.AddObserver(message.Entity, message.Observable, message.Observer)),
                _messageObservable.OfType<With.Message.IIgnore>().Do(Instrument.Harmonize.Action.Ignore).Subscribe(message => _serviceEndpoint.RemoveObserver(message.Entity, message.Observable, message.Observer))
            );
        }

        public Task Start()
        {
            _messageSubscription = _messageObservable.Connect();
            _serviceEndpoint.Start(_messages);

            return TaskEx.Done;
        }

        public Task Stop()
        {
            if (_messageSubscription != null)
            {
                _messageSubscription.Dispose();
                _messageSubscription = null;
            }

            _serviceEndpoint.Stop();

            return TaskEx.Done;
        }

        public async Task Cleanup()
        {
            if (_consumerSubscription != null)
            {
                _consumerSubscription.Dispose();
                _consumerSubscription = null;
            }

            await _serviceEndpoint.Cleanup();
        }
    }
}
