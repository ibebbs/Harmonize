
namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Client.Configuration
{
    public interface IProvider
    {
        ISettings GetSettings();
    }

    internal class Provider : IProvider
    {
        public ISettings GetSettings()
        {
            return new Settings { HarmonizeSignalRUrl = "http://localhost:8999", HarmonizeHubName = "HarmonizeHub" };
        }
    }
}
