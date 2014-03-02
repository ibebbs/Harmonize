
namespace Bebbs.Harmonize.With.Harmony.Hub.Session
{
    public interface IInfo
    {
        string ServerIdentity { get; }
        string HubId { get; }
        string Identity { get; }
        string Status { get; }
        string ProductId { get; }
        string XmppVersion { get; }
        string HttpVersion { get; }
        string RfVersion { get; }
        string HarmonyVersion { get; } 
        string FriendlyName { get; }
    }

    internal class Info : IInfo
    {
        public Info(string serverIdentity, string hubId, string identity, string status, string productId,
            string xmppVersion, string httpVersion, string rfVersion, string harmonyVersion, string friendlyName)
        {
            ServerIdentity = serverIdentity;
            HubId = hubId;
            Identity = identity;
            Status = status;
            ProductId = productId;
            XmppVersion = xmppVersion;
            HttpVersion = httpVersion;
            RfVersion = rfVersion;
            HarmonyVersion = harmonyVersion;
            FriendlyName = friendlyName;
        }

        public string ServerIdentity { get; private set; }
        public string HubId { get; private set; }
        public string Identity { get; private set; }
        public string Status { get; private set; }
        public string ProductId { get; private set; }
        public string XmppVersion { get; private set; }
        public string HttpVersion { get; private set; }
        public string RfVersion { get; private set; }
        public string HarmonyVersion { get; private set; }
        public string FriendlyName { get; private set; }
    }
}
