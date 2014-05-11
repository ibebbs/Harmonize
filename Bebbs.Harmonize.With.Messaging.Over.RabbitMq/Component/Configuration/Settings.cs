
namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Component.Configuration
{
    public interface ISettings : Common.Configuration.ISettings
    {
    }

    public class Settings : ISettings
    {
        public string HostName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ExchangeName { get; set; }
    }

}
