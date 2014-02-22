using System.Linq;
using System.Xml.Linq;
using agsXMPP.protocol.client;
using agsXMPP.Xml.Dom;

namespace Bebbs.Harmonize.With.Harmony.Services
{
    public interface IXmppService
    {
        IQ ConstructSessionInfoRequest(string authenticationToken);

        string ExtractSessionRequestResponse(IQ iQ);

        IQ ConstructConfigurationRequest();

        Hub.Configuration.IValues ExtractHarmonyConfiguration(ISessionInfo sessionInfo, IQ iQ);

        IQ ConstructCommand(string deviceId, string command);
    }

    internal class XmppService : IXmppService
    {
        private const string XmppQueryActionElement = "oa";
        private const string XmppMimeAttribute = "mime";
        private const string XmppErrorCodeAttribute = "errorcode";
        private const string XmppErrorCodeForOk = "200";

        private const string SessionTokenRequestFormat = "token={0}:name={1}#{2}#{3}";
        private const string LogitechConnectNamespace = "connect.logitech.com";
        private const string SessionRequestPath = "vnd.logitech.connect/vnd.logitech.pair";
        private const string ConfigurationRequestPath = "vnd.logitech.harmony/vnd.logitech.harmony.engine?config";
        private const string ExecuteCommandPath = "vnd.logitech.harmony/vnd.logitech.harmony.engine?holdAction";
        private const string ExecuteCommandPattern = "action={{\"type\"::\"IRCommand\",\"deviceId\"::\"{0}\",\"command\"::\"{1}\"}}:status=press";
        private const string SessionName = "1vm7ATw/tN6HXGpQcCs/A5MkuvI";
        private const string SessionOs = "iOS6.0.1";
        private const string SessionDevice = "iPhone";

        private readonly Hub.Configuration.IParser _configurationParser;

        public XmppService(Hub.Configuration.IParser configurationParser)
        {
            _configurationParser = configurationParser;
        }

        public IQ ConstructSessionInfoRequest(string authenticationToken)
        {
            string token = string.Format(SessionTokenRequestFormat, authenticationToken, SessionName, SessionOs, SessionDevice);

            Element query = new Element(XmppQueryActionElement, token, LogitechConnectNamespace);
            query.Attributes.Add(XmppMimeAttribute, SessionRequestPath);

            IQ message = new IQ(IqType.get);
            message.Id = agsXMPP.Id.GetNextId();
            message.Query = query;

            return message;
        }

        public string ExtractSessionRequestResponse(IQ iQ)
        {
            XDocument queryResponse = XDocument.Parse(iQ.InnerXml);
            XNamespace ns = XNamespace.Get(LogitechConnectNamespace);

            string result = queryResponse.Descendants(ns + XmppQueryActionElement).Where(element =>
                element.Attributes(XmppErrorCodeAttribute).Where(attribute => string.Equals(attribute.Value, XmppErrorCodeForOk)).Any() &&
                element.Attributes(XmppMimeAttribute).Where(attribute => string.Equals(attribute.Value, SessionRequestPath)).Any()
            ).Select(element => element.Value).FirstOrDefault();

            return result;
        }

        public IQ ConstructConfigurationRequest()
        {
            Element query = new Element(XmppQueryActionElement, string.Empty, LogitechConnectNamespace);
            query.Attributes.Add(XmppMimeAttribute, ConfigurationRequestPath);

            IQ message = new IQ(IqType.get);
            message.Id = agsXMPP.Id.GetNextId();
            message.Query = query;

            return message;
        }

        public Hub.Configuration.IValues ExtractHarmonyConfiguration(ISessionInfo sessionInfo, IQ iQ)
        {
            if (!string.IsNullOrWhiteSpace(iQ.InnerXml))
            {
                XDocument queryResponse = XDocument.Parse(iQ.InnerXml);
                XNamespace ns = XNamespace.Get(LogitechConnectNamespace);

                string result = queryResponse.Descendants(ns + XmppQueryActionElement).Where(element =>
                    element.Attributes(XmppErrorCodeAttribute).Where(attribute => string.Equals(attribute.Value, XmppErrorCodeForOk)).Any() &&
                    element.Attributes(XmppMimeAttribute).Where(attribute => string.Equals(attribute.Value, ConfigurationRequestPath)).Any()
                ).Select(element => element.Value).FirstOrDefault();

                return _configurationParser.FromJson(sessionInfo.FriendlyName, result);
            }
            else
            {
                return null;
            }
        }

        public IQ ConstructCommand(string deviceId, string command)
        {
            string action = string.Format(ExecuteCommandPattern, deviceId, command);

            Element query = new Element(XmppQueryActionElement, action, LogitechConnectNamespace);
            query.Attributes.Add(XmppMimeAttribute, ExecuteCommandPath);

            IQ message = new IQ(IqType.get);
            message.Id = agsXMPP.Id.GetNextId();
            message.Query = query;

            return message;
        }
    }
}
