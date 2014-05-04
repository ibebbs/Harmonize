
namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Client.Connection
{
    public interface IFactory
    {
        IInstance Create();
    }

    internal class Factory : IFactory
    {
        private readonly Common.IConnectionFactory _connectionFactory;
        private readonly Common.Configuration.ISettings _configurationSettings;
        private readonly Consumer.IFactory _consumerFactory;
        private readonly Producer.IFactory _producerFactory;
        private readonly Common.Routing.IKey _routingKey;
        private readonly Common.Queue.IName _queueName;

        public Factory(Common.IConnectionFactory connectionFactory, Common.Configuration.ISettings configurationSettings, Consumer.IFactory consumerFactory, Producer.IFactory producerFactory, Common.Routing.IKey routingKey, Common.Queue.IName queueName)
        {
            _connectionFactory = connectionFactory;
            _configurationSettings = configurationSettings;
            _consumerFactory = consumerFactory;
            _producerFactory = producerFactory;
            _routingKey = routingKey;
            _queueName = queueName;
        }

        public IInstance Create()
        {
            Instance instance = new Instance(_connectionFactory, _configurationSettings, _consumerFactory, _producerFactory, _routingKey, _queueName);

            instance.Connect();

            return instance;
        }
    }
}
