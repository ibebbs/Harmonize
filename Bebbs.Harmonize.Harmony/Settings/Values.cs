using System;

namespace Bebbs.Harmonize.With.Harmony.Settings
{
    public interface IValues
    {
        string EMail { get; }
        string Password { get; }
        Uri AuthenticationEndpoint { get; }
        string HarmonyHubAddress { get; }
    }

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
