using agsXMPP;
using agsXMPP.protocol.sasl;
using Bebbs.Harmonize.With.Harmony.Hub.Stanza;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Harmony.Hub.Endpoint
{
    public interface IInstance
    {
        Task ConnectAsync();
        Task DisconnectAsync();
        Task<Configuration.IValues> GetHarmonyConfigurationAsync();

        IObservable<agsXMPP.protocol.client.Message> Messages { get; }
        IObservable<agsXMPP.protocol.client.IQ> Iq { get; }
    }

    internal class Instance : IInstance
    {
        private const string SessionJidPattern = "{0}@x.com";
        private const string SessionPasswordPattern = "{0}";

        private readonly string _connectServer;
        private readonly Session.IInfo _sessionInfo;

        private readonly Subject<agsXMPP.protocol.client.Message> _messages;
        private readonly Subject<agsXMPP.protocol.client.IQ> _iq;

        private XmppClientConnection _connection;
        private IDisposable _messageSubscription;
        private IDisposable _iqSubscription;

        public Instance(string server, Session.IInfo sessionInfo)
        {
            _connectServer = server;
            _sessionInfo = sessionInfo;

            _messages = new Subject<agsXMPP.protocol.client.Message>();
            _iq = new Subject<agsXMPP.protocol.client.IQ>();
        }

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

        private void OnLogin(object sender)
        {
            Instrumentation.Xmpp.LogIn();
        }

        public Task ConnectAsync()
        {
            _connection = new XmppClientConnection();

            Jid sessionJid = new Jid(string.Format(SessionJidPattern, _sessionInfo.Identity));
            string sessionPassword = string.Format(SessionPasswordPattern, _sessionInfo.Identity);

            _connection.Server = sessionJid.Server;
            _connection.Username = sessionJid.User;
            _connection.Password = sessionPassword;

            _connection.AutoResolveConnectServer = false;
            _connection.ForceStartTls = false;
            _connection.ConnectServer = _connectServer; // values.HarmonyHubAddress;
            _connection.AutoAgents = false;
            _connection.AutoPresence = false;
            _connection.AutoRoster = false;            
            _connection.OnSaslStart += OnSaslStart;
            _connection.OnSaslEnd += OnSaslEnd;
            _connection.OnXmppConnectionStateChanged += (s, e) => Instrumentation.Xmpp.ConnectionStateChanged(e);
            _connection.OnReadXml += (s, e) => Instrumentation.Xmpp.Receive(e);
            _connection.OnWriteXml += (s, e) => Instrumentation.Xmpp.Transmit(e);
            _connection.OnError += (s, e) => Instrumentation.Xmpp.Error(e);
            _connection.OnSocketError += (s, e) => Instrumentation.Xmpp.SocketError(e);

            _messageSubscription = Observable.FromEvent<agsXMPP.protocol.client.MessageHandler, agsXMPP.protocol.client.Message>(handler => (s, e) => handler(e), handler => _connection.OnMessage += handler, handler => _connection.OnMessage -= handler)
                                             .Do(msg => Instrumentation.Xmpp.MessageReceived(msg.From, msg.To, msg.Id, msg.Error, msg.Type, msg.Subject, msg.Thread, msg.Body))
                                             .Subscribe(_messages);

            _iqSubscription = Observable.FromEvent<agsXMPP.protocol.client.IqHandler, agsXMPP.protocol.client.IQEventArgs>(handler => (s, e) => handler(e), handler => _connection.OnIq += handler, handler => _connection.OnIq -= handler)
                                        .Do(args => Instrumentation.Xmpp.IqReceived(args.IQ.From, args.IQ.To, args.IQ.Id, args.IQ.Error, args.IQ.Type, args.IQ.Value))
                                        .Do(args => args.Handled = true)
                                        .Select(args => args.IQ)
                                        .Subscribe(_iq);

            Task connected = Observable.FromEvent<agsXMPP.ObjectHandler, Unit>(handler => s => handler(Unit.Default), handler => _connection.OnLogin += handler, handler => _connection.OnLogin -= handler)
                                       .Timeout(TimeSpan.FromSeconds(30))
                                       .Take(1)
                                       .ToTask();

            _connection.Open();

            return connected;
        }

        public Task DisconnectAsync()
        {
            if (_iqSubscription != null)
            {
                _iqSubscription.Dispose();
                _iqSubscription = null;
            }

            if (_messageSubscription != null)
            {
                _messageSubscription.Dispose();
                _messageSubscription = null;
            }

            if (_connection != null)
            {
                _connection.Close();
                _connection = null;
            }

            return Task.FromResult<object>(null);
        }

        public Task<Configuration.IValues> GetHarmonyConfigurationAsync()
        {
            Task<Configuration.IValues> configuration = _iq.HarmonyConfiguration(_sessionInfo).Timeout(TimeSpan.FromSeconds(10)).Take(1).ToTask();

            _connection.Send(Builder.ConstructConfigurationRequest());

            return configuration;
        }

        public IObservable<agsXMPP.protocol.client.Message> Messages 
        {
            get { return _messages; }
        }

        public IObservable<agsXMPP.protocol.client.IQ> Iq
        {
            get { return _iq; }
        }
    }
}
