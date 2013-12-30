
namespace Bebbs.Harmonize.Harmony.Messages
{
    public interface IStartedMessage : IMessage
    {
        Harmony.Hub.Configuration.IValues HarmonyConfiguration { get; }
    }

    internal class StartedMessage : IStartedMessage
    {
        public StartedMessage(Harmony.Hub.Configuration.IValues harmonyConfiguration)
        {
            HarmonyConfiguration = harmonyConfiguration;
        }

        public Harmony.Hub.Configuration.IValues HarmonyConfiguration { get; private set; }
    }
}
