using RabbitMQ.Client;

namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Common
{
    public interface IConnectionFactory
    {
        IConnection CreateConnection();
    }

    public class ConnectionFactory : IConnectionFactory
    {
        private readonly RabbitMQ.Client.ConnectionFactory _connectionFactory;

        public ConnectionFactory(Configuration.ISettings configurationSettings)
        {
            _connectionFactory = new RabbitMQ.Client.ConnectionFactory()
            {
                HostName = configurationSettings.HostName,
                UserName = configurationSettings.UserName,
                Password = configurationSettings.Password
            };
        }

        public IConnection CreateConnection()
        {
            return _connectionFactory.CreateConnection();
        }
    }
}
