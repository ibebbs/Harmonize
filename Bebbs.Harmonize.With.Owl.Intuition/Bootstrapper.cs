﻿using EventSourceProxy;
using Ninject;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Owl.Intuition
{
    [EventSourceImplementation(Name = "Bebbs-Harmonize-With-Owl-Intuition")]
    public interface IBootstrapper : Host.IService
    {

    }

    public class Bootstrapper : IBootstrapper
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

        public async Task Initialize()
        {
            await _clientEndpoint.Initialize();

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

        public async Task Cleanup()
        {
            _connector.Cleanup();

            await _clientEndpoint.Cleanup();
        }
    }
}
