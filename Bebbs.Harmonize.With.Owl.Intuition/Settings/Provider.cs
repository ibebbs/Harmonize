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
        private static readonly Lazy<IValues> SettingsValues = new Lazy<IValues>(() => new Values(new IPEndPoint(IPAddress.Loopback, 5100), new IPEndPoint(IPAddress.Loopback, 5110), "11AA22BB", new IPEndPoint(IPAddress.Parse("192.168.1.30"), 5100), TimeSpan.FromSeconds(10), true));

        public IValues GetValues()
        {
            return SettingsValues.Value;
        }
    }
}
