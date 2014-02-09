using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Bebbs.Harmonize.With.Messaging
{
    public interface IBridge : IInitialize, ICleanup
    {

    }

    internal class Bridge : IBridge
    {
        private readonly IGlobalEventAggregator _eventAggregator;
        private readonly IMapping _mapper;
        private readonly IWrapper _wrapper;
        private readonly IEnumerable<IEndpoint> _endpoints;

        private IDisposable _inboundSubscription;
        private IDisposable _outboundSubscription;

        public Bridge(IGlobalEventAggregator eventAggregator, IMapping mapper, IWrapper wrapper, IEnumerable<IEndpoint> endpoints)
        {
            _eventAggregator = eventAggregator;
            _mapper = mapper;
            _wrapper = wrapper;
            _endpoints = (endpoints ?? Enumerable.Empty<IEndpoint>()).ToArray();
        }

        private void Publish(Schema.Message message)
        {
            _endpoints.ForEach(endpoint => endpoint.Publish(message));
        }

        public void Initialize()
        {
            var source = Observable.Merge(_endpoints.Select(endpoint => endpoint.Messages)).Select(message => message.Item);

            _inboundSubscription = new CompositeDisposable(
                source.OfType<Schema.Register>().Do(Instrumentation.Messages.Received).Select(_mapper.ToComponent).Subscribe(_eventAggregator.Publish),
                source.OfType<Schema.Deregister>().Do(Instrumentation.Messages.Received).Select(_mapper.ToComponent).Subscribe(_eventAggregator.Publish),
                source.OfType<Schema.Observe>().Do(Instrumentation.Messages.Received).Select(_mapper.ToComponent).Subscribe(_eventAggregator.Publish),
                source.OfType<Schema.Subscribe>().Do(Instrumentation.Messages.Received).Select(_mapper.ToComponent).Subscribe(_eventAggregator.Publish),
                source.OfType<Schema.Act>().Do(Instrumentation.Messages.Received).Select(_mapper.ToComponent).Subscribe(_eventAggregator.Publish)
            );

            _outboundSubscription = new CompositeDisposable(
                _eventAggregator.GetEvent<With.Message.IRegister>().Select(_mapper.ToMessage).Do(Instrumentation.Messages.Sent).Select(_wrapper.Wrap).Subscribe(Publish),
                _eventAggregator.GetEvent<With.Message.IDeregister>().Select(_mapper.ToMessage).Do(Instrumentation.Messages.Sent).Select(_wrapper.Wrap).Subscribe(Publish),
                _eventAggregator.GetEvent<With.Message.IObservation>().Select(_mapper.ToMessage).Do(Instrumentation.Messages.Sent).Select(_wrapper.Wrap).Subscribe(Publish),
                _eventAggregator.GetEvent<With.Message.IAct>().Select(_mapper.ToMessage).Do(Instrumentation.Messages.Sent).Select(_wrapper.Wrap).Subscribe(Publish)
            );
        }

        public void Cleanup()
        {
            if (_inboundSubscription != null)
            {
                _inboundSubscription.Dispose();
                _inboundSubscription = null;
            }

            if (_outboundSubscription != null)
            {
                _outboundSubscription.Dispose();
                _outboundSubscription = null;
            }
        }
    }
}
