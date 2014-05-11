using System;
using System.Linq;
using System.Net;

namespace Bebbs.Harmonize.With.Store.Using.EventStore.Configuration
{
    public interface ISettings
    {
        With.Component.IComponent Component { get; }
        IPAddress Host { get; }
        int Port { get; }
        string Stream { get; }
    }

    public class Settings : ISettings
    {
        private readonly Lazy<With.Component.IComponent> InternalComponent = new Lazy<Component.IComponent>(() => new With.Component.Component(With.Component.Identity.Unique(), new With.Component.ComponentDescription()));

        With.Component.IComponent ISettings.Component
        {
            get { return InternalComponent.Value; } 
        }

        IPAddress ISettings.Host
        {
            get { return Dns.GetHostAddresses(Host).FirstOrDefault(); }
        }

        public string Host { get; set; }
        public int Port { get; set; }
        public string Stream { get; set; }
    }
}
