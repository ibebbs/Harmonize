
namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Service.Connection
{
    public interface IFactory
    {
        IInstance Create();
    }

    internal class Factory : IFactory
    {
        private readonly Common.IConnectionFactory _connectionFactory;

        public Factory(Common.IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IInstance Create()
        {
            Instance instance = new Instance(_connectionFactory);
            instance.Connect();

            return instance;
        }
    }
}
