
namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Common.Connection
{
    public interface IFactory
    {
        IInstance Create(Configuration.ISettings configurationSettings);
    }

    internal class Factory : IFactory
    {
        private readonly Common.IConnectionFactory _connectionFactory;
        private readonly With.Message.ISerializer _messageSerializer;

        public Factory(Common.IConnectionFactory connectionFactory, With.Message.ISerializer messageSerializer)
        {
            _connectionFactory = connectionFactory;
            _messageSerializer = messageSerializer;
        }

        public IInstance Create(Configuration.ISettings configurationSettings)
        {
            Instance instance = new Instance(configurationSettings, _connectionFactory, _messageSerializer);
            instance.Connect();

            return instance;
        }
    }
}
