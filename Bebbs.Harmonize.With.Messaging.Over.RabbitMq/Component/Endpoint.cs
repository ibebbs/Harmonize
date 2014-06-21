using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Component
{
    internal class Endpoint : With.Messaging.Component.IEndpoint
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

        public void Add(With.Component.IIdentity registrar, With.Component.IComponent component, IObserver<Message.IMessage> consumer)
        {
            _connectionInstance.BuildQueue(_queueName.For(component.Identity));
            _connectionInstance.BindConsumer(_queueName.For(component.Identity), consumer);
            _connectionInstance.Publish(_configurationSettings.ExchangeName, _routingKey.ForAdditionOf(component.Identity), new Message.Add(registrar, component));
        }

        public void Remove(With.Component.IIdentity registrar, With.Component.IIdentity component)
        {
            _connectionInstance.Publish(_configurationSettings.ExchangeName, _routingKey.ForRemovalOf(component), new Message.Remove(registrar, component));
            _connectionInstance.RemoveQueue(_queueName.For(component));
        }
    }
}
