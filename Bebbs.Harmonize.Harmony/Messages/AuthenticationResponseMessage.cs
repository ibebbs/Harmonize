
namespace Bebbs.Harmonize.With.Harmony.Messages
{
    public interface IAuthenticationResponseMessage : IMessage
    {
        bool Success { get; }
        string AccountId { get; }
        string AuthenticationToken { get; }
        int ErrorCode { get; }
        string Message { get; }
        string Source { get; }
    }

    public class AuthenticationResponseMessage : IAuthenticationResponseMessage
    {
        public AuthenticationResponseMessage(string accountId, string authenticationToken)
        {
            Success = true;
            AccountId = accountId;
            AuthenticationToken = authenticationToken;
        }

        public AuthenticationResponseMessage(int errorCode, string message, string source)
        {
            Success = false;
            ErrorCode = errorCode;
            Message = message;
            Source = source;
        }

        public bool Success { get; private set; }
        public string AccountId { get; private set; }
        public string AuthenticationToken { get; private set; }
        public int ErrorCode { get; private set; }
        public string Message { get; private set; }
        public string Source { get; private set; }
    }
}
