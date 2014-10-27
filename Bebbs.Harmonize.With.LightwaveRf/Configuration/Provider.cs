using Bebbs.Harmonize.With.Component;
using EventSourceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using Config = SimpleConfig.Configuration;

namespace Bebbs.Harmonize.With.LightwaveRf.Configuration
{
    [EventSourceImplementation(Name = "Bebbs-Harmonize-With-LightwaveRf-Configuration-Provider")]
    public interface IProvider
    {
        ISettings GetSettings();

        IEnumerable<IDevice> GetDevices();
    }
    
    internal class Provider : IProvider
    {
        private static readonly Lazy<WifiLink> WifiLink = new Lazy<WifiLink>(() => Config.Load<WifiLink>(sectionName: "wifiLink"));

        public ISettings GetSettings()
        {
            return WifiLink.Value.Settings;
        }

        public IEnumerable<IDevice> GetDevices()
        {
            return WifiLink.Value.Devices.Dimmers;
        }
    }
}
