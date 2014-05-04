using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Service.Connection
{
    public interface IInstance : IDisposable
    {
        void BuildExchange(string exchangeName);

        void BuildQueue(string queueName);

        void RemoveQueue(string queueName);

        void RemoveExchange(string exchangeName);

        IRoute Route(string routingKey);
    }

    internal class Instance : IInstance
    {
        private readonly Common.IConnectionFactory _connectionFactory;

        private RabbitMQ.Client.IConnection _connection;
        private RabbitMQ.Client.IModel _model;

        public Instance(Common.IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
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
            _connection = _connectionFactory.CreateConnection();
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

        public void BuildQueue(string queueName)
        {
            _model.QueueDeclare(queueName, false, true, true, null);
        }

        public void RemoveQueue(string queueName)
        {
            _model.QueueDelete(queueName);
        }

        public void RemoveExchange(string exchangeName)
        {
            _model.ExchangeDelete(exchangeName);
        }

        public IRoute Route(string routingKey)
        {
            return new Route(BindRoute, UnbindRoute, routingKey);
        }
    }
}
