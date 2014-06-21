using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Ninject;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Ninject;
using Owin;
using System;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Service
{
    public class Bootstrapper : Host.IService
    {
        private readonly IKernel _kernel;

        private NinjectDependencyResolver _dependencyResolver;
        private IHarmonizeConnector _connector;
        private IDisposable _webApp;

        public Bootstrapper(IKernel kernel)
        {
            _kernel = kernel;
        }

        private void Configuration(IAppBuilder app)
        {
            var config = new HubConfiguration()
            {
                Resolver = _dependencyResolver
            };

            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR(config);
        }

        public Task Initialize()
        {
            _kernel.Load(new Module());

            _dependencyResolver = new NinjectDependencyResolver(_kernel);

            GlobalHost.DependencyResolver = _dependencyResolver;

            _connector = _kernel.Get<IHarmonizeConnector>();
            return _connector.Initialize();
        }

        public Task Start()
        {
            string url = "http://localhost:8999";

            StartOptions options = new StartOptions(url);

            _webApp = WebApp.Start(options, Configuration);

            Console.WriteLine("Server running on {0}", url);

            return Task.FromResult<object>(null);
        }

        public Task Stop()
        {
            if (_webApp != null)
            {
                _webApp.Dispose();
                _webApp = null;
            }

            return Task.FromResult<object>(null);
        }

        public Task Cleanup()
        {
            return _connector.Cleanup();
        }
    }
}
