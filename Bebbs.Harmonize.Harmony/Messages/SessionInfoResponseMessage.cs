
namespace Bebbs.Harmonize.With.Harmony.Messages
{
    public interface ISessionInfoResponseMessage : IMessage
    {
        ISessionInfo SessionInfo { get; }
    }

    public class SessionInfoResponseMessage : ISessionInfoResponseMessage
    {
        public SessionInfoResponseMessage(ISessionInfo sessionInfo)
        {
            SessionInfo = sessionInfo;
        }

        public ISessionInfo SessionInfo { get; private set; }
    }
}
