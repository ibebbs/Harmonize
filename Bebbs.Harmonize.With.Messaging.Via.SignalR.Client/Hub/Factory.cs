using Microsoft.AspNet.SignalR.Client;
using System.IO;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Client.Hub
{
    public interface IFactory
    {
        IInstance Create();
        IInstance Create(TextWriter hubDebug);
    }

    internal class Factory : IFactory
    {
        private readonly Configuration.ISettings _settings;

        public Factory(Configuration.ISettings settings)
        {
            _settings = settings;
        }

        public IInstance Create(TextWriter hubDebug)
        {
            HubConnection hubConnection = new HubConnection(_settings.HarmonizeSignalRUrl);

            if (hubDebug != null)
            {
                hubConnection.TraceLevel = TraceLevels.All;
                hubConnection.TraceWriter = hubDebug;
            }

            IHubProxy hubProxy = hubConnection.CreateHubProxy(_settings.HarmonizeHubName);

            return new Instance(hubConnection, hubProxy);
        }

        public IInstance Create()
        {
            return Create(null);
        }
    }
}
