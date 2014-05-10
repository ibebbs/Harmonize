using Ninject;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Owl.Intuition
{
    public class Bootstrapper : Host.IService
    {
        private readonly Messaging.Client.IEndpoint _clientEndpoint;
        private readonly Configuration.Provider _configurationProvider;
        private readonly Gateway.Factory _gatewayFactory;
        private readonly Connector _connector;

        public Bootstrapper(IKernel kernel, With.Messaging.Client.IEndpoint clientEndpoint)
        {
            _clientEndpoint = clientEndpoint;

            _configurationProvider = new Configuration.Provider();
            _gatewayFactory = new Gateway.Factory(kernel);

            _connector = new Connector(_clientEndpoint, _configurationProvider.GetSettings(), _gatewayFactory);
        }

        public void Initialize()
        {
            _clientEndpoint.Initialize();

            _connector.Initialize();
        }

        public Task Start()
        {
            return _connector.Start();
        }

        public Task Stop()
        {
            return _connector.Stop();
        }

        public void Cleanup()
        {
            _connector.Cleanup();

            _clientEndpoint.Cleanup();
        }
    }
}
