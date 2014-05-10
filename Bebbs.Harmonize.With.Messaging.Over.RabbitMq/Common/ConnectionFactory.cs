using RabbitMQ.Client;

namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Common
{
    public interface IConnectionFactory
    {
        IConnection CreateConnection(Configuration.ISettings configurationSettings);
    }

    public class ConnectionFactory : IConnectionFactory
    {
        public IConnection CreateConnection(Configuration.ISettings configurationSettings)
        {
            RabbitMQ.Client.ConnectionFactory connectionFactory = new RabbitMQ.Client.ConnectionFactory()
            {
                HostName = configurationSettings.HostName,
                UserName = configurationSettings.UserName,
                Password = configurationSettings.Password
            };

            return connectionFactory.CreateConnection();
        }
    }
}
