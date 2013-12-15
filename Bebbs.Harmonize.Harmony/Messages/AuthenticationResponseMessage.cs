
namespace Bebbs.Harmonize.Harmony.Messages
{
    public interface IAuthenticationResponseMessage : IMessage
    {
        string AccountId { get; }
        string AuthenticationToken { get; }
    }

    public class AuthenticationResponseMessage : IAuthenticationResponseMessage
    {
        public AuthenticationResponseMessage(string accountId, string authenticationToken)
        {
            AccountId = accountId;
            AuthenticationToken = authenticationToken;
        }

        public string AccountId { get; private set; }
        public string AuthenticationToken { get; private set; }
    }
}
