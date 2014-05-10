using System;

namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Service
{
    internal class Endpoint : Messaging.Service.IEndpoint
    {
        private readonly Configuration.ISettings _configurationSettings;
        private readonly Common.Connection.IFactory _connectionFactory;
        private readonly Common.Routing.IKey _routingKey;
        private readonly Common.Queue.IName _queueName;

        private Common.Connection.IInstance _connectionInstance;

        public Endpoint(Configuration.ISettings configurationSettings, Common.Connection.IFactory connectionFactory, Common.Routing.IKey routingKey, Common.Queue.IName queueName)
        {
            _configurationSettings = configurationSettings;
            _connectionFactory = connectionFactory;
            _routingKey = routingKey;
            _queueName = queueName;
        }

        public void Initialize()
        {
            _connectionInstance = _connectionFactory.Create(_configurationSettings);
        }

        public void Cleanup()
        {
            if (_connectionInstance != null)
            {
                _connectionInstance.Dispose();
                _connectionInstance = null;
            }
        }

        public void Start(IObserver<Message.IMessage> consumer)
        {
            _connectionInstance.BuildExchange(_configurationSettings.ExchangeName);
            _connectionInstance.BuildQueue(_configurationSettings.QueueName);
            _connectionInstance.Route(_routingKey.ForRegistration()).From(_configurationSettings.ExchangeName).To(_configurationSettings.QueueName).Start();
            _connectionInstance.Route(_routingKey.ForObserve()).From(_configurationSettings.ExchangeName).To(_configurationSettings.QueueName).Start();
        }

        public void Stop()
        {
            _connectionInstance.Route(_routingKey.ForObserve()).From(_configurationSettings.ExchangeName).To(_configurationSettings.QueueName).Stop();
            _connectionInstance.Route(_routingKey.ForRegistration()).From(_configurationSettings.ExchangeName).To(_configurationSettings.QueueName).Stop();
            _connectionInstance.RemoveQueue(_configurationSettings.QueueName);
            _connectionInstance.RemoveExchange(_configurationSettings.ExchangeName);
        }

        public void Register(Component.IIdentity entity)
        {
            _connectionInstance.Route(_routingKey.ForActionOf(entity)).From(_configurationSettings.ExchangeName).To(_queueName.For(entity)).Start();
            //_connectionInstance.PublishRegistered(entity);
        }

        public void Deregister(Component.IIdentity entity)
        {
            //_connectionInstance.PublishDeregistered(entity);
            _connectionInstance.Route(_routingKey.ForActionOf(entity)).From(_configurationSettings.ExchangeName).To(_queueName.For(entity)).Stop();
        }

        public void AddObserver(Component.IIdentity sourceEntity, Component.IIdentity observable, Component.IIdentity targetEntity)
        {
            _connectionInstance.Route(_routingKey.ForObservationOf(sourceEntity, observable)).From(_configurationSettings.ExchangeName).To(_queueName.For(targetEntity)).Start();
        }

        public void RemoveObserver(Component.IIdentity sourceEntity, Component.IIdentity observable, Component.IIdentity targetEntity)
        {
            _connectionInstance.Route(_routingKey.ForObservationOf(sourceEntity, observable)).From(_configurationSettings.ExchangeName).To(_queueName.For(targetEntity)).Stop();
        }
    }
}
