using Bebbs.Harmonize.With.Message;
using RabbitMQ.Client;
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Client.Connection.Consumer
{
    public interface IInstance : IBasicConsumer
    {

    }

    internal class Instance : IInstance
    {
        private readonly IHelper _messagingHelper;
        private readonly Subject<byte[]> _deliveries;

        public event RabbitMQ.Client.Events.ConsumerCancelledEventHandler ConsumerCancelled;

        public Instance(Messaging.IHelper messagingHelper, IModel model, IObserver<IMessage> consumer)
        {
            Model = model;

            _deliveries = new Subject<byte[]>();
            _deliveries.Select(messagingHelper.Deserialize).Subscribe(consumer);
        }

        public void HandleBasicCancel(string consumerTag)
        {
            // Do nothing
        }

        public void HandleBasicCancelOk(string consumerTag)
        {
            // Do nothing
        }

        public void HandleBasicConsumeOk(string consumerTag)
        {
            // Do nothing
        }

        public void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, byte[] body)
        {
            _deliveries.OnNext(body);
        }

        public void HandleModelShutdown(IModel model, ShutdownEventArgs reason)
        {
            _deliveries.OnCompleted();
        }

        public IModel Model { get; private set; }
    }
}
