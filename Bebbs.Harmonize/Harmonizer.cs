using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Bebbs.Harmonize
{
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

        public void Initialize()
        {
            _serviceEndpoint.Initialize();

            _consumerSubscription = new CompositeDisposable(
                _messageObservable.OfType<With.Message.IRegister>().Subscribe(message => _serviceEndpoint.Register(message.Entity.Identity)),
                _messageObservable.OfType<With.Message.IDeregister>().Subscribe(message => _serviceEndpoint.Deregister(message.Entity)),
                _messageObservable.OfType<With.Message.IObserve>().Subscribe(message => _serviceEndpoint.AddObserver(message.Entity, message.Observable, message.Observer)),
                _messageObservable.OfType<With.Message.IIgnore>().Subscribe(message => _serviceEndpoint.RemoveObserver(message.Entity, message.Observable, message.Observer))
            );
        }

        public Task Start()
        {
            _messageSubscription = _messageObservable.Connect();
            _serviceEndpoint.Start(_messages);

            return Task.FromResult<object>(null);
        }

        public Task Stop()
        {
            if (_messageSubscription != null)
            {
                _messageSubscription.Dispose();
                _messageSubscription = null;
            }

            _serviceEndpoint.Stop();

            return Task.FromResult<object>(null);
        }

        public void Cleanup()
        {
            if (_consumerSubscription != null)
            {
                _consumerSubscription.Dispose();
                _consumerSubscription = null;
            }

            _serviceEndpoint.Cleanup();
        }
    }
}
