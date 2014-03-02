using System;

namespace Bebbs.Harmonize.With.Harmony.Settings
{
    public interface IProvider
    {
        IValues GetValues();
    }

    internal class Provider : IProvider
    {
        public IValues GetValues()
        {
            return new Values("<UserName>", "<Password>", new Uri("https://svcs.myharmony.com/CompositeSecurityServices/Security.svc/json/GetUserAuthToken"), "<IpAddress>");
        }
    }
}
