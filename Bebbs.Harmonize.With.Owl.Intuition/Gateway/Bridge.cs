using Bebbs.Harmonize.With.Component;
using System;
using System.Reactive.Disposables;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway
{
    public interface IBridge : IInitialize, ICleanup
    {

    }

    internal class Bridge : IBridge
    {
        private static readonly IIdentity Identity = new StringIdentity("Bebbs.Harmonize.With.Owl.Intuition");

        private readonly Event.IMediator _eventMediator;
        private readonly With.IGlobalEventAggregator _eventAggregator;

        private IDisposable _mediatorSubscription;

        public Bridge(Event.IMediator eventMediator, With.IGlobalEventAggregator eventAggregator)
        {
            _eventMediator = eventMediator;
            _eventAggregator = eventAggregator;
        }

        private void Process(Event.Register registration)
        {
            _eventAggregator.Publish(new Message.Register(Identity, registration.Entity));
        }

        private void Process(Event.Observation observation)
        {
            _eventAggregator.Publish(new Message.Observation(observation.EntityIdentity, observation.ObservableIdentity, observation.AsOf, observation.Measurement));
        }

        private void Process(Event.Deregister deregistration)
        {
            _eventAggregator.Publish(new Message.Deregister(Identity, deregistration.Entity.Identity));
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
