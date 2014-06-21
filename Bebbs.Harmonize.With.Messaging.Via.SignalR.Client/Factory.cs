using Bebbs.Harmonize.With.Messaging.Client;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Client
{
    public interface IFactory
    {
        IEndpoint Create(string connectionString, string hubName);
    }

    public class Factory : IFactory
    {
        public static readonly IFactory Default = new Factory();

        public IEndpoint Create(string connectionString, string hubName)
        {
            return new Endpoint(
                new Hub.Factory(
                    new Configuration.Settings
                    {
                        HarmonizeSignalRUrl = connectionString,
                        HarmonizeHubName = hubName
                    }
                ),
                new Registration.Factory(),
                new Mapper()
            );
        }
    }
}
