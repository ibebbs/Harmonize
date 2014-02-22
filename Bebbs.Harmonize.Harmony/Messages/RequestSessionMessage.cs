
namespace Bebbs.Harmonize.With.Harmony.Messages
{
    public interface IRequestSessionMessage : IMessage
    {
        ISessionInfo SessionInfo { get; }
    }

    public class RequestSessionMessage : IRequestSessionMessage
    {
        public RequestSessionMessage(ISessionInfo sessionInfo)
        {
            SessionInfo = sessionInfo;
        }

        public ISessionInfo SessionInfo { get; private set; }
    }
}
