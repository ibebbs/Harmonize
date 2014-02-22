
namespace Bebbs.Harmonize.With.Harmony.Messages
{
    public interface IRequestHarmonyConfigurationMessage
    {
        ISessionInfo SessionInfo { get; }
        ISession Session { get; }
    }

    internal class RequestHarmonyConfigurationMessage : IRequestHarmonyConfigurationMessage
    {
        public RequestHarmonyConfigurationMessage(ISessionInfo sessionInfo, ISession session)
        {
            SessionInfo = sessionInfo;
            Session = session;
        }
        
        public ISessionInfo SessionInfo { get; private set; }

        public ISession Session { get; private set; }
    }
}
