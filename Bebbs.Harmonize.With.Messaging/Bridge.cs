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
        private readonly Mapping.IHelper _mapper;
        private readonly ISerializer _serializer;
        private readonly IWrapper _wrapper;
        private readonly IEnumerable<IEndpoint> _endpoints;

        private IDisposable _inboundSubscription;
        private IDisposable _outboundSubscription;

        public Bridge(IGlobalEventAggregator eventAggregator, Mapping.IHelper mapper, ISerializer serializer, IWrapper wrapper, IEnumerable<IEndpoint> endpoints)
        {
            _eventAggregator = eventAggregator;
            _mapper = mapper;
            _serializer = serializer;
            _wrapper = wrapper;
            _endpoints = (endpoints ?? Enumerable.Empty<IEndpoint>()).ToArray();
        }

        private void Publish(Schema.Message message)
        {
            _endpoints.OfType<IMessageEndpoint>().ForEach(endpoint => endpoint.Publish(message));

            if (_endpoints.OfType<ISerializedEndpoint>().Any())
            {
                string serialized = _serializer.Serialize(message);

                _endpoints.OfType<ISerializedEndpoint>().ForEach(endpoint => endpoint.Publish(serialized));
            }
        }

        public void Initialize()
        {
            IEnumerable<IObservable<Schema.Message>> serializedEndpoints = _endpoints.OfType<ISerializedEndpoint>().Do(endpoint => endpoint.Initialize()).Select(endpoint => endpoint.Messages.Select(_serializer.Deserialize));
            IEnumerable<IObservable<Schema.Message>> messagingEndpoints = _endpoints.OfType<IMessageEndpoint>().Do(endpoint => endpoint.Initialize()).Select(endpoint => endpoint.Messages);

            var source = Observable.Merge(serializedEndpoints.Concat(messagingEndpoints).ToArray()).Select(message => message.Item);

            _inboundSubscription = new CompositeDisposable(
                source.OfType<Schema.Register>().Do(Instrumentation.Messages.Received).Select(_mapper.ToComponent).Subscribe(_eventAggregator.Publish),
                source.OfType<Schema.Deregister>().Do(Instrumentation.Messages.Received).Select(_mapper.ToComponent).Subscribe(_eventAggregator.Publish),
                source.OfType<Schema.Observe>().Do(Instrumentation.Messages.Received).Select(_mapper.ToComponent).Subscribe(_eventAggregator.Publish),
                source.OfType<Schema.Subscribe>().Do(Instrumentation.Messages.Received).Select(_mapper.ToComponent).Subscribe(_eventAggregator.Publish),
                source.OfType<Schema.Act>().Do(Instrumentation.Messages.Received).Select(_mapper.ToComponent).Subscribe(_eventAggregator.Publish)
            );

            _outboundSubscription = new CompositeDisposable(
                _eventAggregator.GetEvent<With.Message.IRegister>().Select(_mapper.ToSchema).Do(Instrumentation.Messages.Sent).Select(_wrapper.Wrap).Subscribe(Publish),
                _eventAggregator.GetEvent<With.Message.IDeregister>().Select(_mapper.ToSchema).Do(Instrumentation.Messages.Sent).Select(_wrapper.Wrap).Subscribe(Publish),
                _eventAggregator.GetEvent<With.Message.IObservation>().Select(_mapper.ToSchema).Do(Instrumentation.Messages.Sent).Select(_wrapper.Wrap).Subscribe(Publish),
                _eventAggregator.GetEvent<With.Message.IAct>().Select(_mapper.ToSchema).Do(Instrumentation.Messages.Sent).Select(_wrapper.Wrap).Subscribe(Publish)
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

            _endpoints.ForEach(endpoint => endpoint.Cleanup());
        }
    }
}
