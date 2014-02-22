
namespace Bebbs.Harmonize.With.Harmony.Messages
{
    public interface IHarmonyConfigurationResponseMessage
    {
        Hub.Configuration.IValues HarmonyConfiguration { get; }
    }

    internal class HarmonyConfigurationResponseMessage : IHarmonyConfigurationResponseMessage
    {
        public HarmonyConfigurationResponseMessage(Hub.Configuration.IValues harmonyConfiguration)
        {
            HarmonyConfiguration = harmonyConfiguration;
        }

        public Hub.Configuration.IValues HarmonyConfiguration { get; private set; }
    }
}
