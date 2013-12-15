using System;

namespace Bebbs.Harmonize.Common.Settings
{
    public interface IValues
    {
        string EMail { get; }
        string Password { get; }
        Uri AuthenticationEndpoint { get; }
        string HarmonyHubAddress { get; }
    }
}
