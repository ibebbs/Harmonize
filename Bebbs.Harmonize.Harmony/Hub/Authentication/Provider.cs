using agsXMPP;
using agsXMPP.protocol.sasl;
using Bebbs.Harmonize.With.Harmony.Hub.Stanza;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Harmony.Hub.Authentication
{
    public interface IProvider
    {
        Task<Session.IInfo> AuthenticateAsync(string connectServer, string authenticationToken);
    }

    internal class Provider : IProvider
    {
        private static readonly Jid Guest = new Jid("guest@connect.logitech.com/gatorade.");
        private const string GuestPassword = "gatorade.";
        private const string SessionJidPattern = "{0}@x.com";
        private const string SessionPasswordPattern = "{0}";
        
        private IDisposable _iqSubscription;

        private void OnSaslEnd(object sender)
        {
            Instrumentation.Xmpp.SaslEnd();
        }

        private void OnSaslStart(object sender, agsXMPP.Sasl.SaslEventArgs args)
        {
            Instrumentation.Xmpp.SaslStart();

            args.Auto = false;

            args.Mechanism = agsXMPP.protocol.sasl.Mechanism.GetMechanismName(MechanismType.PLAIN);
        }

        public async Task<Session.IInfo> AuthenticateAsync(string connectServer, string authenticationToken)
        {
            XmppClientConnection connection = new XmppClientConnection();

            connection.Server = Guest.Server;
            connection.Username = Guest.User;
            connection.Password = GuestPassword;

            connection.AutoResolveConnectServer = false;
            connection.ForceStartTls = false;
            connection.ConnectServer = connectServer; // values.HarmonyHubAddress;
            connection.AutoAgents = false;
            connection.AutoPresence = false;
            connection.AutoRoster = false;
            connection.OnSaslStart += OnSaslStart;
            connection.OnSaslEnd += OnSaslEnd;
            connection.OnXmppConnectionStateChanged += (s, e) => Instrumentation.Xmpp.ConnectionStateChanged(e);
            connection.OnReadXml += (s, e) => Instrumentation.Xmpp.Receive(e);
            connection.OnWriteXml += (s, e) => Instrumentation.Xmpp.Transmit(e);
            connection.OnError += (s, e) => Instrumentation.Xmpp.Error(e);
            connection.OnSocketError += (s, e) => Instrumentation.Xmpp.SocketError(e);

            Task connected = Observable.FromEvent<agsXMPP.ObjectHandler, Unit>(handler => s => handler(Unit.Default), handler => connection.OnLogin += handler, handler => connection.OnLogin -= handler)
                                       .Timeout(TimeSpan.FromSeconds(30))
                                       .Take(1)
                                       .ToTask();

            connection.Open();

            await connected;

            Task<Session.IInfo> session =
                Observable.FromEvent<agsXMPP.protocol.client.IqHandler, agsXMPP.protocol.client.IQEventArgs>(handler => (s, e) => handler(e), handler => connection.OnIq += handler, handler => connection.OnIq -= handler)
                      .Do(args => Instrumentation.Xmpp.IqReceived(args.IQ.From, args.IQ.To, args.IQ.Id, args.IQ.Error, args.IQ.Type, args.IQ.Value))
                      .Do(args => args.Handled = true)
                      .Select(args => args.IQ)
                      .SessionResponses()
                      .Timeout(TimeSpan.FromSeconds(10))
                      .Take(1)
                      .ToTask();

            connection.Send(Builder.ConstructSessionInfoRequest(authenticationToken));

            Session.IInfo sessionInfo = await session;

            connection.Close();

            return sessionInfo;
        }
    }
}
