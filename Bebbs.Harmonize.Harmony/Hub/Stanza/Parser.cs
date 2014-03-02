using agsXMPP.protocol.client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Bebbs.Harmonize.With.Harmony.Hub.Stanza
{
    internal static class Parser
    {
        private const string SessionResponseRegexPattern = @"^(?:(?:serverIdentity=(?<ServerIdentity>.{36}):?)|(?:hubId=(?<HubId>\d{1,4}):?)|(?:identity=(?<Identity>[^:]*):?)|(?:status=(?<Status>[^:]*):?)|(?:protocolVersion={(?:(?:XMPP=""(?<XmppVersion>\d+.\d+)""(?:,\W+)?)|(?:HTTP=""(?<HttpVersion>\d+.\d+)""(?:,\W+)?)|(?:RF=""(?<RfVersion>\d+.\d+)""(?:,\W+)?))*}:?)|(?:hubProfiles={(?:(?:Harmony=""(?<HarmonyVersion>\d+.\d+)""(?:,\W+)?))*}:?)|(?:productId=(?<ProductId>[^:]*):?)|(?:friendlyName=(?<FriendlyName>[^:]+):?))*$";
        private const string SessionResponseServerIdentityGroup = "ServerIdentity";
        private const string SessionResponseHubIdGroup = "HubId";
        private const string SessionResponseIdentityGroup = "Identity";
        private const string SessionResponseStatusGroup = "Status";
        private const string SessionResponseProductIdGroup = "ProductId";
        private const string SessionResponseXmppVersionGroup = "XmppVersion";
        private const string SessionResponseHttpVersionGroup = "HttpVersion";
        private const string SessionResponseRfVersionGroup = "RfVersion";
        private const string SessionResponseHarmonyVersionGroup = "HarmonyVersion";
        private const string SessionResponseFriendlyNameGroup = "FriendlyName";

        private static readonly Regex SessionResponseRegex = new Regex(SessionResponseRegexPattern, RegexOptions.Compiled);

        private static string ExtractSessionString(string xml)
        {
            XDocument queryResponse = XDocument.Parse(xml);
            XNamespace ns = XNamespace.Get(Constants.LogitechConnectNamespace);

            string result = queryResponse.Descendants(ns + Constants.XmppQueryActionElement).Where(element =>
                element.Attributes(Constants.XmppErrorCodeAttribute).Where(attribute => string.Equals(attribute.Value, Constants.XmppErrorCodeForOk)).Any() &&
                element.Attributes(Constants.XmppMimeAttribute).Where(attribute => string.Equals(attribute.Value, Constants.SessionRequestPath)).Any()
            ).Select(element => element.Value).FirstOrDefault();

            return result;
        }

        private static string ExtractConfigurationString(string xml)
        {
            XDocument queryResponse = XDocument.Parse(xml);
            XNamespace ns = XNamespace.Get(Constants.LogitechConnectNamespace);

            string result = queryResponse.Descendants(ns + Constants.XmppQueryActionElement).Where(element =>
                element.Attributes(Constants.XmppErrorCodeAttribute).Where(attribute => string.Equals(attribute.Value, Constants.XmppErrorCodeForOk)).Any() &&
                element.Attributes(Constants.XmppMimeAttribute).Where(attribute => string.Equals(attribute.Value, Constants.ConfigurationRequestPath)).Any()
            ).Select(element => element.Value).FirstOrDefault();

            return result;
        }

        private static IEnumerable<Session.IInfo> MatchSessionInfo(string session)
        {
            Match match = SessionResponseRegex.Matches(session).OfType<Match>().FirstOrDefault();

            if (match != null)
            {
                return new Session.IInfo[] { 
                    new Session.Info(
                        match.Groups[SessionResponseServerIdentityGroup].Value,
                        match.Groups[SessionResponseHubIdGroup].Value,
                        match.Groups[SessionResponseIdentityGroup].Value,
                        match.Groups[SessionResponseStatusGroup].Value,
                        match.Groups[SessionResponseProductIdGroup].Value,
                        match.Groups[SessionResponseXmppVersionGroup].Value,
                        match.Groups[SessionResponseHttpVersionGroup].Value,
                        match.Groups[SessionResponseRfVersionGroup].Value,
                        match.Groups[SessionResponseHarmonyVersionGroup].Value,
                        match.Groups[SessionResponseFriendlyNameGroup].Value
                    )
                };
            }
            else
            {
                return Enumerable.Empty<Session.IInfo>();
            }
        }

        public static IObservable<Configuration.IValues> HarmonyConfiguration(this IObservable<IQ> source, Session.IInfo sessionInfo)
        {
            return source.Select(iq => iq.InnerXml)
                         .Except(string.IsNullOrWhiteSpace)
                         .Select(ExtractConfigurationString)
                         .Except(string.IsNullOrWhiteSpace)
                         .Select(value => Configuration.Parser.FromJson(sessionInfo.FriendlyName, value));
        }

        public static IObservable<Session.IInfo> SessionResponses(this IObservable<IQ> source)
        {
            return source.Select(iq => iq.InnerXml)
                         .Except(string.IsNullOrWhiteSpace)
                         .Select(ExtractSessionString)
                         .Except(string.IsNullOrWhiteSpace)
                         .SelectMany(MatchSessionInfo);
        }
    }
}
