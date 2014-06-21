using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Client
{
    internal class Endpoint : Messaging.Client.IEndpoint
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

        public void Register(With.Component.IIdentity registrar, With.Component.IEntity entity, IObserver<With.Message.IMessage> consumer)
        {
            _connectionInstance.BuildQueue(_queueName.For(entity.Identity));
            _connectionInstance.BindConsumer(_queueName.For(entity.Identity), consumer);
            _connectionInstance.Publish(_configurationSettings.ExchangeName, _routingKey.ForRegistrationOf(entity.Identity), new Message.Register(registrar, entity));
        }

        public void Deregister(With.Component.IIdentity registrar, With.Component.IEntity entity)
        {
            _connectionInstance.Publish(_configurationSettings.ExchangeName, _routingKey.ForDeregistrationOf(entity.Identity), new Message.Deregister(registrar, entity.Identity));
            _connectionInstance.RemoveQueue(_queueName.For(entity.Identity));
        }

        public void Publish(With.Component.IIdentity entity, With.Component.IIdentity observable, DateTimeOffset date, With.Component.IMeasurement measurement)
        {
            _connectionInstance.Publish(_configurationSettings.ExchangeName, _routingKey.ForObservationBy(entity, observable), new Message.Observation(entity, observable, date, measurement));
        }

        public void Perform(With.Component.IIdentity actor, With.Component.IIdentity entity, With.Component.IIdentity actionable, IEnumerable<With.Component.IParameterValue> parameterValues)
        {
            _connectionInstance.Publish(_configurationSettings.ExchangeName, _routingKey.ForActionBy(entity, actionable), new Message.Action(entity, actionable, actor, parameterValues));
        }
    }
}
