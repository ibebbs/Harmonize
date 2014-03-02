
namespace Bebbs.Harmonize.With.Harmony.Messages
{
    public interface IHarmonyCommandMessage : IMessage
    {
        Hub.Session.IInstance Session { get; }

        string DeviceId { get; }

        string Command { get; }
    }

    internal class HarmonyCommandMessage : IHarmonyCommandMessage
    {
        public HarmonyCommandMessage(Hub.Session.IInstance session, string deviceId, string command)
        {
            Session = session;
            DeviceId = deviceId;
            Command = command;
        }

        public Hub.Session.IInstance Session { get; private set; }

        public string DeviceId { get; private set; }

        public string Command { get; private set; }
    }
}
