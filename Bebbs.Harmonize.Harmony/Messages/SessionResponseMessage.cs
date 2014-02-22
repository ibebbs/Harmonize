
namespace Bebbs.Harmonize.With.Harmony.Messages
{
    public interface ISessionResponseMessage : IMessage
    {
        ISession Session { get; }

        ISessionInfo SessionInfo { get; }
    }

    public class SessionResponseMessage : ISessionResponseMessage
    {
        public SessionResponseMessage(ISession session, ISessionInfo sessionInfo)
        {
            Session = session;
            SessionInfo = sessionInfo;
        }

        public ISession Session { get; private set; }

        public ISessionInfo SessionInfo { get; private set; }
    }
}
