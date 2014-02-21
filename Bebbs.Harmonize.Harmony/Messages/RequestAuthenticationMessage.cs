
namespace Bebbs.Harmonize.With.Harmony.Messages
{
    public interface IRequestAuthenticationMessage : IMessage
    {
        string EMail { get; }
        string Password { get; }
    }

    public class RequestAuthenticationMessage : IRequestAuthenticationMessage
    {
        public RequestAuthenticationMessage(string eMail, string password)
        {
            EMail = eMail;
            Password = password;
        }

        public string EMail { get; private set; }
        public string Password { get; private set; }
    }
}
