
namespace Bebbs.Harmonize.Harmony.Messages
{
    public interface IRequestHarmonyConfigurationMessage
    {
        ISession Session { get; }
    }

    internal class RequestHarmonyConfigurationMessage : IRequestHarmonyConfigurationMessage
    {
        public RequestHarmonyConfigurationMessage(ISession session)
        {
            Session = session;
        }

        public ISession Session { get; private set; }
    }
}
