using System;
using System.Net;

namespace Bebbs.Harmonize.With.Owl.Intuition.Settings
{
    public interface IProvider
    {
        IValues GetValues();
    }

    internal class Provider : IProvider
    {
        private static readonly Lazy<IValues> SettingsValues = new Lazy<IValues>(() => new Values(new IPEndPoint(IPAddress.Parse("192.168.1.105"), 5100), new IPEndPoint(IPAddress.Parse("192.168.1.105"), 5110), "6C9DB92A", new IPEndPoint(IPAddress.Parse("192.168.1.113"), 5100), TimeSpan.FromSeconds(10), true));

        public IValues GetValues()
        {
            return SettingsValues.Value;
        }
    }
}
