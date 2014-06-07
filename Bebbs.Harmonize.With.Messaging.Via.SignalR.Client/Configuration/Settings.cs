using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Client.Configuration
{
    public interface ISettings
    {
        string HarmonizeSignalRUrl { get; }

        string HarmonizeHubName { get; }
    }

    internal class Settings : ISettings
    {
        public string HarmonizeSignalRUrl { get; set; }

        public string HarmonizeHubName { get; set; }
    }
}
