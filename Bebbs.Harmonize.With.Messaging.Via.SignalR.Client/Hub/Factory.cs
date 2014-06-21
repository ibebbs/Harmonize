using Microsoft.AspNet.SignalR.Client;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Client.Hub
{
    public interface IFactory
    {
        IInstance Create();
    }

    internal class Factory : IFactory
    {
        private readonly Configuration.ISettings _settings;

        public Factory(Configuration.ISettings settings)
        {
            _settings = settings;
        }

        public IInstance Create()
        {
            HubConnection hubConnection = new HubConnection(_settings.HarmonizeSignalRUrl);
            IHubProxy hubProxy = hubConnection.CreateHubProxy(_settings.HarmonizeHubName);

            return new Instance(hubConnection, hubProxy);
        }
    }
}
