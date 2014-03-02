using agsXMPP;
using agsXMPP.protocol.client;
using EventSourceProxy;
using System;

namespace Bebbs.Harmonize.With.Harmony
{
    [EventSourceImplementation(Name = "Bebbs-Harmonize-Harmony")]
    public interface IHarmonyState
    {
        void EnteringState(State.Name state);

        void EnteredState(State.Name state);

        void ExitingState(State.Name state);

        void ExitedState(State.Name state);
    }

    [EventSourceImplementation(Name = "Bebbs-Harmonize-Harmony-Xmpp")]
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

    public static class Instrumentation
    {
        public static readonly IHarmonyState State = EventSourceImplementer.GetEventSourceAs<IHarmonyState>();

        public static readonly IXmppEventSource Xmpp = EventSourceImplementer.GetEventSourceAs<IXmppEventSource>();
    }
}
