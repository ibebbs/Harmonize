using Bebbs.Harmonize.With.Settings;
using System;

namespace Bebbs.Harmonize.Service.Settings
{
    internal class Values : IValues
    {
        public Values(string email, string password, Uri authenticationEndpoint, string harmonyHubAddress)
        {
            EMail = email;
            Password = password;
            AuthenticationEndpoint = authenticationEndpoint;
            HarmonyHubAddress = harmonyHubAddress;
        }

        public string EMail { get; private set; }
        public string Password { get; private set; }
        public Uri AuthenticationEndpoint { get; private set; }
        public string HarmonyHubAddress { get; private set; }
    }
}
