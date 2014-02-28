using Bebbs.Harmonize.With.Settings;
using System;

namespace Bebbs.Harmonize.Service.Settings
{
    internal class Provider : IProvider
    {
        public IValues GetValues()
        {
            return new Values("<UserName>", "<Password>", new Uri("https://svcs.myharmony.com/CompositeSecurityServices/Security.svc/json/GetUserAuthToken"), "<IPAddress>");
        }
    }
}
