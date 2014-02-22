
namespace Bebbs.Harmonize.With.Harmony.Messages
{
    public interface IRequestSessionInfoMessage
    {
        string AuthenticationToken { get; }
    }

    public class RequestSessionInfoMessage : IRequestSessionInfoMessage
    {
        public RequestSessionInfoMessage(string authenticationToken)
        {
            AuthenticationToken = authenticationToken;
        }

        public string AuthenticationToken { get; private set; }
    }
}
