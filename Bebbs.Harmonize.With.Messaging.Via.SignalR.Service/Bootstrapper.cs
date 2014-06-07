using Microsoft.Owin.Hosting;
using System;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Service
{
    public class Bootstrapper : Host.IService
    {
        private IDisposable _webApp;

        public void Initialize()
        {
            // Do nothing
        }

        public Task Start()
        {
            string url = "http://localhost:8080";
            _webApp = WebApp.Start(url);

            Console.WriteLine("Server running on {0}", url);
            Console.ReadLine();

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

        public void Cleanup()
        {
            // Do nothing
        }
    }
}
