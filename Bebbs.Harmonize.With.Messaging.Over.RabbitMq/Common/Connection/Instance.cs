using System;

namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Common.Connection
{
    public interface IInstance : IDisposable
    {
        void BuildExchange(string exchangeName);

        void Publish(string exchangeName, string routingKey, With.Message.IMessage message);

        void RemoveExchange(string exchangeName);

        void BuildQueue(string queueName);

        void BindConsumer(string queueName, IObserver<With.Message.IMessage> consumer);

        void RemoveQueue(string queueName);

        IRoute Route(string routingKey);
    }

    internal class Instance : IInstance
    {
        private readonly Configuration.ISettings _configurationSettings;
        private readonly Common.IConnectionFactory _connectionFactory;
        private readonly With.Message.ISerializer _messageSerializer;

        private RabbitMQ.Client.IConnection _connection;
        private RabbitMQ.Client.IModel _model;

        public Instance(Configuration.ISettings configurationSettings, Common.IConnectionFactory connectionFactory, With.Message.ISerializer messageSerializer)
        {
            _configurationSettings = configurationSettings;
            _connectionFactory = connectionFactory;
            _messageSerializer = messageSerializer;
        }

        private void BindRoute(string routingKey, string exchangeName, string queueName)
        {
            _model.QueueBind(queueName, exchangeName, routingKey, null);
        }

        private void UnbindRoute(string routingKey, string exchangeName, string queueName)
        {
            _model.QueueUnbind(queueName, exchangeName, routingKey, null);
        }

        public void Connect()
        {
            _connection = _connectionFactory.CreateConnection(_configurationSettings);
            _model = _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_model != null)
            {
                _model.Dispose();
                _model = null;
            }

            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }

        public void BuildExchange(string exchangeName)
        {
            _model.ExchangeDeclare(exchangeName, RabbitMQ.Client.ExchangeType.Topic);
        }

        public void Publish(string exchangeName, string routingKey, With.Message.IMessage message)
        {
            Message.Producer.Publish(exchangeName, routingKey, _messageSerializer, _model, message);
        }

        public void RemoveExchange(string exchangeName)
        {
            _model.ExchangeDelete(exchangeName);
        }

        public void BuildQueue(string queueName)
        {
            _model.QueueDeclare(queueName, false, false, true, null);
        }

        public void BindConsumer(string queueName, IObserver<With.Message.IMessage> consumer)
        {
            _model.BasicConsume(queueName, true, Message.Consumer.Subscribe(_messageSerializer, _model, consumer));
        }

        public void RemoveQueue(string queueName)
        {
            _model.QueueDelete(queueName);
        }

        public IRoute Route(string routingKey)
        {
            return new Route(BindRoute, UnbindRoute, routingKey);
        }
    }
}
