
namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Common.Configuration
{
    public interface ISettings
    {
        string HostName { get; }

        string UserName { get; }

        string Password { get; }

        string ExchangeName { get; }
    }
}
