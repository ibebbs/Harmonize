using EventSourceProxy;
using Ninject;
using System;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.LightwaveRf
{
    [EventSourceImplementation(Name = "Bebbs-Harmonize-With-LightwaveRf")]
    public interface IBootstrapper : Host.IService
    {

    }

    public class Bootstrapper : IBootstrapper
    {
        private readonly Messaging.Client.IEndpoint _clientEndpoint;
        private readonly Configuration.IProvider _configurationProvider;

        public Bootstrapper(IKernel kernel, With.Messaging.Client.IEndpoint clientEndpoint)
        {
            _clientEndpoint = clientEndpoint;

            _configurationProvider = new Configuration.Provider();
        }

        public async Task Initialize()
        {
            await _clientEndpoint.Initialize();

            throw new NotImplementedException();
        }

        public Task Start()
        {
            throw new NotImplementedException();
        }

        public Task Stop()
        {
            throw new NotImplementedException();
        }

        public async Task Cleanup()
        {
            throw new NotImplementedException();

            await _clientEndpoint.Cleanup();
        }
    }
}
