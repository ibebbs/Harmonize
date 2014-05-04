using Bebbs.Harmonize.With.Message;
using RabbitMQ.Client;
using System;

namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Client.Connection
{
    public interface IInstance : IDisposable
    {
        void BuildQueueForEntity(With.Component.IEntity entity);

        void BindConsumerToQueueForEntity(With.Component.IEntity entity, IObserver<IMessage> consumer);

        void RegisterEntity(With.Component.IEntity entity);

        void DeregisterEntity(With.Component.IEntity entity);

        void DestroyQueueForEntity(With.Component.IEntity entity);

        void Publish(With.Message.IObservation observation);

        void Perform(With.Message.IAct action);
    }

    internal class Instance : IInstance
    {
        private readonly Common.IConnectionFactory _connectionFactory;
        private readonly Common.Configuration.ISettings _configurationSettings;
        private readonly Consumer.IFactory _consumerFactory;
        private readonly Producer.IFactory _producerFactory;
        private readonly Common.Routing.IKey _routingKey;
        private readonly Common.Queue.IName _queueName;

        private IConnection _connection;
        private IModel _model;
        private Producer.IInstance _producer;

        public Instance(Common.IConnectionFactory connectionFactory, Common.Configuration.ISettings configurationSettings, Consumer.IFactory consumerFactory, Producer.IFactory producerFactory, Common.Routing.IKey routingKey, Common.Queue.IName queueName)
        {
            _connectionFactory = connectionFactory;
            _configurationSettings = configurationSettings;
            _consumerFactory = consumerFactory;
            _producerFactory = producerFactory;
            _routingKey = routingKey;
            _queueName = queueName;
        }

        public void Connect()
        {
            _connection = _connectionFactory.CreateConnection();
            _model = _connection.CreateModel();

            _producer = _producerFactory.CreateProducer(_model);
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

        public void BuildQueueForEntity(With.Component.IEntity entity)
        {
            _model.QueueDeclare(_queueName.For(entity.Identity), false, true, true, null);
        }

        public void BindConsumerToQueueForEntity(With.Component.IEntity entity, IObserver<IMessage> consumer)
        {
            Consumer.IInstance instance = _consumerFactory.CreateConsumer(_model, consumer);

            _model.BasicConsume(_queueName.For(entity.Identity), true, instance);
        }

        public void RegisterEntity(With.Component.IEntity entity)
        {
            byte[] body = _producer.BuildRegistration(entity);

            IBasicProperties properties = _model.CreateBasicProperties();

            _model.BasicPublish(_configurationSettings.ExchangeName, _routingKey.ForRegistrationOf(entity.Identity), properties, body);
        }

        public void DeregisterEntity(With.Component.IEntity entity)
        {
            byte[] body = _producer.BuildDeregistration(entity);

            IBasicProperties properties = _model.CreateBasicProperties();

            _model.BasicPublish(_configurationSettings.ExchangeName, _routingKey.ForDeregistrationOf(entity.Identity), properties, body);
        }

        public void DestroyQueueForEntity(With.Component.IEntity entity)
        {
            _model.QueueDelete(_queueName.For(entity.Identity));
        }

        public void Publish(With.Message.IObservation observation)
        {
            byte[] body = _producer.BuildObservation(observation);

            IBasicProperties properties = _model.CreateBasicProperties();

            _model.BasicPublish(_configurationSettings.ExchangeName, _routingKey.ForObservation(observation), properties, body);
        }

        public void Perform(With.Message.IAct action)
        {
            byte[] body = _producer.BuildAction(action);

            IBasicProperties properties = _model.CreateBasicProperties();

            _model.BasicPublish(_configurationSettings.ExchangeName, _routingKey.ForAction(action), properties, body);
        }
    }
}
