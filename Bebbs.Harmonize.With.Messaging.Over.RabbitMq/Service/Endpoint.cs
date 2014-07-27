using EventSourceProxy;
using System;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Service
{
    [EventSourceImplementation(Name = "Bebbs-Harmonize-With-Messaging-Over-RabbitMq-Service-Endpoint")]
    public interface IEndpoint : Messaging.Service.IEndpoint
    {

    }

    internal class Endpoint : IEndpoint
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

        public Task Initialize()
        {
            _connectionInstance = _connectionFactory.Create(_configurationSettings);

            return TaskEx.Done;
        }

        public Task Cleanup()
        {
            if (_connectionInstance != null)
            {
                _connectionInstance.Dispose();
                _connectionInstance = null;
            }

            return TaskEx.Done;
        }

        public void Start(IObserver<Message.IMessage> consumer)
        {
            _connectionInstance.BuildExchange(_configurationSettings.ExchangeName);
            _connectionInstance.BuildQueue(_configurationSettings.QueueName);
            _connectionInstance.BindConsumer(_configurationSettings.QueueName, consumer);
            _connectionInstance.Route(_routingKey.ForRegistration()).From(_configurationSettings.ExchangeName).To(_configurationSettings.QueueName).Start();
            _connectionInstance.Route(_routingKey.ForComponent()).From(_configurationSettings.ExchangeName).To(_configurationSettings.QueueName).Start();
            _connectionInstance.Route(_routingKey.ForObserve()).From(_configurationSettings.ExchangeName).To(_configurationSettings.QueueName).Start();
        }

        public void Stop()
        {
            _connectionInstance.Route(_routingKey.ForObserve()).From(_configurationSettings.ExchangeName).To(_configurationSettings.QueueName).Stop();
            _connectionInstance.Route(_routingKey.ForComponent()).From(_configurationSettings.ExchangeName).To(_configurationSettings.QueueName).Stop();
            _connectionInstance.Route(_routingKey.ForRegistration()).From(_configurationSettings.ExchangeName).To(_configurationSettings.QueueName).Stop();
            _connectionInstance.RemoveQueue(_configurationSettings.QueueName);
            _connectionInstance.RemoveExchange(_configurationSettings.ExchangeName);
        }

        public void Add(With.Component.IIdentity component)
        {
            _connectionInstance.Route(_routingKey.ForRegistration()).From(_configurationSettings.ExchangeName).To(_queueName.For(component)).Start();
            _connectionInstance.Route(_routingKey.ForObservation()).From(_configurationSettings.ExchangeName).To(_queueName.For(component)).Start();
            _connectionInstance.Route(_routingKey.ForAllActions()).From(_configurationSettings.ExchangeName).To(_queueName.For(component)).Start();
        }

        public void Remove(With.Component.IIdentity component)
        {
            _connectionInstance.Route(_routingKey.ForRegistration()).From(_configurationSettings.ExchangeName).To(_queueName.For(component)).Stop();
            _connectionInstance.Route(_routingKey.ForObservation()).From(_configurationSettings.ExchangeName).To(_queueName.For(component)).Stop();
            _connectionInstance.Route(_routingKey.ForAllActions()).From(_configurationSettings.ExchangeName).To(_queueName.For(component)).Stop();
        }

        public void Register(With.Component.IIdentity entity)
        {
            _connectionInstance.Route(_routingKey.ForActionOf(entity)).From(_configurationSettings.ExchangeName).To(_queueName.For(entity)).Start();
            //_connectionInstance.PublishRegistered(entity);
        }

        public void Deregister(With.Component.IIdentity entity)
        {
            //_connectionInstance.PublishDeregistered(entity);
            _connectionInstance.Route(_routingKey.ForActionOf(entity)).From(_configurationSettings.ExchangeName).To(_queueName.For(entity)).Stop();
        }

        public void AddObserver(With.Component.IIdentity sourceEntity, With.Component.IIdentity observable, With.Component.IIdentity targetEntity)
        {
            _connectionInstance.Route(_routingKey.ForObservationOf(sourceEntity, observable)).From(_configurationSettings.ExchangeName).To(_queueName.For(targetEntity)).Start();
        }

        public void RemoveObserver(With.Component.IIdentity sourceEntity, With.Component.IIdentity observable, With.Component.IIdentity targetEntity)
        {
            _connectionInstance.Route(_routingKey.ForObservationOf(sourceEntity, observable)).From(_configurationSettings.ExchangeName).To(_queueName.For(targetEntity)).Stop();
        }
    }
}
