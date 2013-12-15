
namespace Bebbs.Harmonize.Harmony.Messages
{
    public interface IHarmonyConfigurationResponseMessage
    {
        IHarmonyConfiguration HarmonyConfiguration { get; }
    }

    internal class HarmonyConfigurationResponseMessage : IHarmonyConfigurationResponseMessage
    {
        public HarmonyConfigurationResponseMessage(IHarmonyConfiguration harmonyConfiguration)
        {
            HarmonyConfiguration = harmonyConfiguration;
        }

        public IHarmonyConfiguration HarmonyConfiguration { get; private set; }
    }
}
