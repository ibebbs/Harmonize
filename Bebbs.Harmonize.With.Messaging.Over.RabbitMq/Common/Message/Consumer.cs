using Bebbs.Harmonize.With.Message;
using RabbitMQ.Client;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Common.Message
{
    public interface IConsumer : IBasicConsumer
    {

    }

    internal class Consumer : IConsumer
    {
        public static IConsumer Subscribe(With.Message.ISerializer messageSerializer, IModel model, IObserver<IMessage> consumer)
        {
            return new Consumer(messageSerializer, model, consumer);
        }

        private readonly Subject<byte[]> _deliveries;

        public event RabbitMQ.Client.Events.ConsumerCancelledEventHandler ConsumerCancelled;

        internal Consumer(With.Message.ISerializer messageSerializer, IModel model, IObserver<IMessage> consumer)
        {
            Model = model;

            _deliveries = new Subject<byte[]>();
            _deliveries.ObserveOn(Scheduler.Default).Select(System.Text.Encoding.UTF8.GetString).Select(messageSerializer.Deserialize).Subscribe(consumer);
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
