using agsXMPP.protocol.client;
using agsXMPP.Xml.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Harmony.Hub.Stanza
{
    internal static class Builder
    {
        public static IQ ConstructSessionInfoRequest(string authenticationToken)
        {
            string token = string.Format(Constants.SessionTokenRequestFormat, authenticationToken, Constants.SessionName, Constants.SessionOs, Constants.SessionDevice);

            Element query = new Element(Constants.XmppQueryActionElement, token, Constants.LogitechConnectNamespace);
            query.Attributes.Add(Constants.XmppMimeAttribute, Constants.SessionRequestPath);

            IQ message = new IQ(IqType.get);
            message.Id = agsXMPP.Id.GetNextId();
            message.Query = query;

            return message;
        }

        public static IQ ConstructConfigurationRequest()
        {
            Element query = new Element(Constants.XmppQueryActionElement, string.Empty, Constants.LogitechConnectNamespace);
            query.Attributes.Add(Constants.XmppMimeAttribute, Constants.ConfigurationRequestPath);

            IQ message = new IQ(IqType.get);
            message.Id = agsXMPP.Id.GetNextId();
            message.Query = query;

            return message;
        }
    }
}
