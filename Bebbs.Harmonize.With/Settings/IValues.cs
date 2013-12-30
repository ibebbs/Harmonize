using System;

namespace Bebbs.Harmonize.With.Settings
{
    public interface IValues
    {
        string EMail { get; }
        string Password { get; }
        Uri AuthenticationEndpoint { get; }
        string HarmonyHubAddress { get; }
    }
}
