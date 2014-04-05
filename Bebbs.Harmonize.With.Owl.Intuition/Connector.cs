using Bebbs.Harmonize.With.Serialization;
using Ninject;
using Ninject.Extensions.ChildKernel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Owl.Intuition
{
    public interface IConnector : IInitialize, ICleanup, IStart, IStop
    {

    }

    internal class Connector : IConnector
    {
        private readonly Configuration.IProvider _configurationProvider;
        private readonly Gateway.IFactory _gatewayFactory;

        private Configuration.Details _configuration;
        private IEnumerable<Gateway.IContext> _contexts;

        public Connector(Configuration.IProvider configurationProvider, Gateway.IFactory gatewayFactory)
        {
            _configurationProvider = configurationProvider;
            _gatewayFactory = gatewayFactory;
        }

        private Gateway.IContext Create(Configuration.Device configurationDevice)
        {
            return _gatewayFactory.CreateDeviceInContext(configurationDevice);
        }

        private IEnumerable<Gateway.IContext> LoadInstances()
        {
            try
            {
                _configuration = _configurationProvider.GetConfiguration();

                _contexts = _configuration.Devices.Select(Create).ToArray();

                return _contexts;
            }
            catch (Exception e)
            {
                Instrumentation.Configuration.Error(e.Message);

                return Enumerable.Empty<Gateway.IContext>();
            }
        }

        private void Initialize(Gateway.IContext deviceContext)
        {
            deviceContext.Initialize();
        }

        public void Initialize()
        {
            try
            {
                _contexts = LoadInstances();
                _contexts.ForEach(Initialize);
            }
            catch (Exception e)
            {
                Instrumentation.Configuration.Failure(e.Message);
            }
        }

        public void Cleanup()
        {
            _contexts.ForEach(context => context.Dispose());
            _contexts = null;
        }

        public Task Start()
        {
            Task[] startTasks = _contexts.Select(context => context.Instance.Start()).ToArray();

            return Task.WhenAll(startTasks);
        }

        public Task Stop()
        {
            Task[] stopTasks = _contexts.Select(context => context.Instance.Stop()).ToArray();

            return Task.WhenAll(stopTasks);
        }

        public IEnumerable<Gateway.IContext> Instances 
        {
            get { return _contexts; }
        }
    }
}
