using Bebbs.Harmonize.With.Message;
using RabbitMQ.Client;
using System;

namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Client.Connection.Consumer
{
    public interface IFactory
    {
        IInstance CreateConsumer(IModel model, IObserver<IMessage> observer);
    }

    internal class Factory : IFactory
    {
        private readonly IHelper _messagingHelper;

        public Factory(Messaging.IHelper messagingHelper)
        {
            _messagingHelper = messagingHelper;
        }

        public IInstance CreateConsumer(IModel model, IObserver<IMessage> consumer)
        {
            return new Instance(_messagingHelper, model, consumer);
        }
    }
}
