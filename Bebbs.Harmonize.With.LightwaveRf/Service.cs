using EventSourceProxy;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.LightwaveRf
{
    [EventSourceImplementation(Name = "Bebbs-Harmonize-With-LightwaveRf-Service")]
    public interface IService : Host.IService
    {

    }

    public class Service : IService
    {
        private readonly WifiLink.IBridge _bridge;
        private readonly Entity.IFactory _entityFactory;
        private readonly Configuration.IProvider _configurationProvider;
        private readonly Messaging.Client.IEndpoint _clientEndpoint;

        private IEnumerable<ILightwaveEntity> _entities;

        public Service(WifiLink.IBridge bridge, Entity.IFactory entityFactory, Configuration.IProvider configurationProvider, With.Messaging.Client.IEndpoint clientEndpoint)
        {
            _entityFactory = entityFactory;
            _clientEndpoint = clientEndpoint;
            _configurationProvider = configurationProvider;
            _bridge = bridge;
        }

        public async Task Initialize()
        {
            await _clientEndpoint.Initialize();

            _entities = _configurationProvider.GetDevices().Select(device => _entityFactory.Create(device, _bridge, _clientEndpoint)).ToArray();
        }

        public async Task Start()
        {
            await _bridge.Initialize();

            await Task.WhenAll(_entities.Select(entity => entity.Initialize()).ToArray());
        }

        public async Task Stop()
        {
            await Task.WhenAll(_entities.Select(entity => entity.CleanUp()).ToArray());

            await _bridge.CleanUp();
        }

        public async Task Cleanup()
        {
            _entities = null;

            await _clientEndpoint.Cleanup();
        }
    }
}
