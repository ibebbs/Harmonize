using System;
using agsXMPP;
using agsXMPP.protocol.client;
using EventSourceProxy;

namespace Bebbs.Harmonize.With.Harmony.Services
{
    [EventSourceImplementation(Name = "Cogenity-Harmonize-Harmony-Xmpp")]
    public interface IXmppEventSource
    {
        void SaslStart();

        void SaslEnd();

        void ConnectionStateChanged(XmppConnectionState state);

        void LogIn();

        void Transmit(string xml);

        void Receive(string xml);

        void MessageReceived(Jid from, Jid to, string id, agsXMPP.protocol.client.Error error, MessageType messageType, string subject, string thread, string body);

        void IqReceived(Jid from, Jid to, string id, agsXMPP.protocol.client.Error error, IqType iqType, string value);

        void Error(Exception ex);

        void SocketError(Exception ex);
    }

    public static class XmppEventSource
    {
        public static readonly IXmppEventSource Log = EventSourceImplementer.GetEventSourceAs<IXmppEventSource>();
    }
}
