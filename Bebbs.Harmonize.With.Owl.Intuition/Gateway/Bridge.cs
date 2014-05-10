using Bebbs.Harmonize.With.Component;
using System;
using System.Reactive.Disposables;
using System.Reactive.Subjects;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway
{
    public interface IBridge : IInitialize, ICleanup
    {

    }

    internal class Bridge : IBridge
    {
        private static readonly IIdentity Identity = new Identity("Bebbs.Harmonize.With.Owl.Intuition");

        private readonly Event.IMediator _eventMediator;
        private readonly With.Messaging.Client.IEndpoint _clientEndpoint;
        private readonly Subject<With.Message.IMessage> _messages;

        private IDisposable _mediatorSubscription;

        public Bridge(Event.IMediator eventMediator, With.Messaging.Client.IEndpoint clientEndpoint)
        {
            _eventMediator = eventMediator;
            _clientEndpoint = clientEndpoint;

            _messages = new Subject<Message.IMessage>();
        }

        private void Process(Event.Register registration)
        {
            _clientEndpoint.Register(Identity, registration.Entity, _messages);
        }

        private void Process(Event.Observation observation)
        {
            _clientEndpoint.Publish(observation.EntityIdentity, observation.ObservableIdentity, observation.AsOf, observation.Measurement);
        }

        private void Process(Event.Deregister deregistration)
        {
            _clientEndpoint.Deregister(Identity, deregistration.Entity);
        }

        public void Initialize()
        {
            _mediatorSubscription = new CompositeDisposable(
                _eventMediator.GetEvent<Event.Register>().Subscribe(Process),
                _eventMediator.GetEvent<Event.Observation>().Subscribe(Process),
                _eventMediator.GetEvent<Event.Deregister>().Subscribe(Process)
            );
        }

        public void Cleanup()
        {
            if (_mediatorSubscription != null)
            {
                _mediatorSubscription.Dispose();
                _mediatorSubscription = null;
            }
        }
    }
}
