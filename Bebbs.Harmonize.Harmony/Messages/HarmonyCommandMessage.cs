
namespace Bebbs.Harmonize.With.Harmony.Messages
{
    public interface IHarmonyCommandMessage : IMessage
    {
        ISession Session { get; }

        string DeviceId { get; }

        string Command { get; }
    }

    internal class HarmonyCommandMessage : IHarmonyCommandMessage
    {
        public HarmonyCommandMessage(ISession session, string deviceId, string command)
        {
            Session = session;
            DeviceId = deviceId;
            Command = command;
        }

        public ISession Session { get; private set; }

        public string DeviceId { get; private set; }

        public string Command { get; private set; }
    }
}
