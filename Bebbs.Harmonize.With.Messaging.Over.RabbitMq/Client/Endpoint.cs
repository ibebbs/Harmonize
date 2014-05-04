using RabbitMQ.Client;
using System;
using System.Reactive.Linq;

namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Client
{
    internal class Endpoint : Messaging.Client.IEndpoint, IInitialize, ICleanup
    {
        private readonly Connection.IFactory _connectionFactory;
        private Connection.IInstance _connectionInstance;

        public Endpoint(Connection.IFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void Initialize()
        {
            _connectionInstance = _connectionFactory.Create();
        }

        public void Cleanup()
        {
            if (_connectionInstance != null)
            {
                _connectionInstance.Dispose();
                _connectionInstance = null;
            }
        }

        public void Register(With.Component.IEntity entity, IObserver<With.Message.IMessage> consumer)
        {
            _connectionInstance.BuildQueueForEntity(entity);
            _connectionInstance.BindConsumerToQueueForEntity(entity, consumer);
            _connectionInstance.RegisterEntity(entity);
        }

        public void Deregister(With.Component.IEntity entity)
        {
            _connectionInstance.DeregisterEntity(entity);
            _connectionInstance.DestroyQueueForEntity(entity);
        }

        public void Publish(With.Message.IObservation observation)
        {
            _connectionInstance.Publish(observation);
        }

        public void Perform(With.Message.IAct action)
        {
            _connectionInstance.Perform(action);
        }
    }
}
