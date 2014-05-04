using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Service.Configuration
{
    public interface ISettings : Common.Configuration.ISettings
    {
        string QueueName { get; }
    }

    public class Settings : ISettings
    {
        public string HostName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ExchangeName { get; set; }

        public string QueueName { get; set; }
    }

}
