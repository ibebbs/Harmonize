using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using agsXMPP;
using agsXMPP.protocol.client;
using agsXMPP.protocol.sasl;
using Bebbs.Harmonize.With.Harmony.Messages;

namespace Bebbs.Harmonize.With.Harmony.Services
{
    public interface IHarmonyService : IInitialize, ICleanup
    {

    }

    internal class HarmonyService : IHarmonyService
    {
        private static readonly Jid Guest = new Jid("guest@connect.logitech.com/gatorade.");
        private const string GuestPassword = "gatorade.";
        private const string SessionJidPattern = "{0}@x.com";
        private const string SessionPasswordPattern = "{0}";

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

        private readonly IGlobalEventAggregator _eventAggregator;
        private readonly With.Settings.IProvider _settingsProvider;
        private readonly IXmppService _xmppService;

        private IDisposable _subscription;

        public HarmonyService(IGlobalEventAggregator eventAggregator, With.Settings.IProvider settingsProvider, IXmppService xmppService)
        {
            _eventAggregator = eventAggregator;
            _settingsProvider = settingsProvider;
            _xmppService = xmppService;
        }

        private void RequestSessionToken(XmppClientConnection connection, string authenticationToken)
        {
            IQ message = _xmppService.ConstructSessionInfoRequest(authenticationToken);

            connection.Send(message);
        }

        private void UsePlainAuthenticationMechanism(agsXMPP.Sasl.SaslEventArgs args)
        {
            args.Auto = false;

            args.Mechanism = agsXMPP.protocol.sasl.Mechanism.GetMechanismName(MechanismType.PLAIN);
        }

        private void ForwardSessionInformationWhenReceived(XmppClientConnection connection, IQEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.IQ.InnerXml))
            {
                string result = _xmppService.ExtractSessionRequestResponse(e.IQ);

                if (!string.IsNullOrWhiteSpace(result))
                {
                    Match match = SessionResponseRegex.Matches(result).OfType<Match>().FirstOrDefault();

                    if (match != null)
                    {
                        SessionInfoResponseMessage response = new SessionInfoResponseMessage(
                            new SessionInfo(
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
                        );

                        e.Handled = true;
                        
                        _eventAggregator.Publish(response);

                        connection.Close();
                    }
                }
            }
        }

        private void SessionStarted(XmppClientConnection connection, ISessionInfo sessionInfo)
        {
            SessionResponseMessage response = new SessionResponseMessage(new Session(connection), sessionInfo);

            _eventAggregator.Publish(response);
        }

        private void PublishHarmonyConfiguration(Hub.Configuration.IValues harmonyConfiguration)
        {
            HarmonyConfigurationResponseMessage response = new HarmonyConfigurationResponseMessage(harmonyConfiguration);

            _eventAggregator.Publish(response);
        }

        private void ProcessRequest(IRequestSessionInfoMessage request)
        {
            With.Settings.IValues values = _settingsProvider.GetValues();

            XmppClientConnection connection = new XmppClientConnection();

            connection.Server = Guest.Server;
            connection.Username = Guest.User;
            connection.Password = GuestPassword;

            connection.AutoResolveConnectServer = false;
            connection.ForceStartTls = false;
            connection.ConnectServer = values.HarmonyHubAddress;
            connection.AutoAgents = false;
            connection.AutoPresence = false;
            connection.AutoRoster = false;
            connection.OnLogin += s => Actions.Run(() => XmppEventSource.Log.LogIn(), () => RequestSessionToken(connection, request.AuthenticationToken));
            connection.OnSaslStart += (s, e) => Actions.Run(() => XmppEventSource.Log.SaslStart(), () => UsePlainAuthenticationMechanism(e));
            connection.OnSaslEnd += s => XmppEventSource.Log.SaslEnd();
            connection.OnXmppConnectionStateChanged += (s,e) => XmppEventSource.Log.ConnectionStateChanged(e);
            connection.OnReadXml += (s, e) => XmppEventSource.Log.Receive(e);
            connection.OnWriteXml += (s,e) => XmppEventSource.Log.Transmit(e);
            connection.OnMessage += (s,e) => XmppEventSource.Log.MessageReceived(e.From, e.To, e.Id, e.Error, e.Type, e.Subject, e.Thread, e.Body);
            connection.OnIq += (s, e) => Actions.Run(() => XmppEventSource.Log.IqReceived(e.IQ.From, e.IQ.To, e.IQ.Id, e.IQ.Error, e.IQ.Type, e.IQ.Value), () => ForwardSessionInformationWhenReceived(connection, e));
            connection.OnError += (s,e) => XmppEventSource.Log.Error(e);
            connection.OnSocketError += (s,e) => XmppEventSource.Log.SocketError(e);

            connection.Open();
        }

        private void ProcessRequest(IRequestSessionMessage request)
        {
            With.Settings.IValues values = _settingsProvider.GetValues();

            Jid sessionJid = new Jid(string.Format(SessionJidPattern, request.SessionInfo.Identity));
            string sessionPassword = string.Format(SessionPasswordPattern, request.SessionInfo.Identity);

            XmppClientConnection connection = new XmppClientConnection();

            connection.Server = sessionJid.Server;
            connection.Username = sessionJid.User;
            connection.Password = sessionPassword;

            connection.AutoResolveConnectServer = false;
            connection.ForceStartTls = false;
            connection.ConnectServer = values.HarmonyHubAddress;
            connection.AutoAgents = false;
            connection.AutoPresence = false;
            connection.AutoRoster = false;
            connection.OnLogin += s => Actions.Run(() => XmppEventSource.Log.LogIn(), () => SessionStarted(connection, request.SessionInfo));
            connection.OnSaslStart += (s, e) => Actions.Run(() => XmppEventSource.Log.SaslStart(), () => UsePlainAuthenticationMechanism(e));
            connection.OnSaslEnd += s => XmppEventSource.Log.SaslEnd();
            connection.OnXmppConnectionStateChanged += (s, e) => XmppEventSource.Log.ConnectionStateChanged(e);
            connection.OnReadXml += (s, e) => XmppEventSource.Log.Receive(e);
            connection.OnWriteXml += (s, e) => XmppEventSource.Log.Transmit(e);
            connection.OnMessage += (s, e) => XmppEventSource.Log.MessageReceived(e.From, e.To, e.Id, e.Error, e.Type, e.Subject, e.Thread, e.Body);
            connection.OnIq += (s, e) => Actions.Run(() => XmppEventSource.Log.IqReceived(e.IQ.From, e.IQ.To, e.IQ.Id, e.IQ.Error, e.IQ.Type, e.IQ.Value));
            connection.OnError += (s, e) => XmppEventSource.Log.Error(e);
            connection.OnSocketError += (s, e) => XmppEventSource.Log.SocketError(e);

            connection.Open();
        }

        private void ProcessRequest(IRequestHarmonyConfigurationMessage request)
        {
            IQ message = _xmppService.ConstructConfigurationRequest();

            Observable.FromEvent<IqHandler, IQEventArgs>(handler => (s,e) => handler(e), handler => request.Session.Connection.OnIq += handler, handler => request.Session.Connection.OnIq -= handler)
                      .Select(iqea => Tuple.Create(iqea, _xmppService.ExtractHarmonyConfiguration(request.SessionInfo, iqea.IQ)))
                      .Where(tuple => tuple.Item2 != null)
                      .Do(tuple => tuple.Item1.Handled = true)
                      .Take(1)
                      .Subscribe(tuple => PublishHarmonyConfiguration(tuple.Item2));

            request.Session.Connection.Send(message);
        }

        private void ProcessCommand(IHarmonyCommandMessage command)
        {
            IQ message = _xmppService.ConstructCommand(command.DeviceId, command.Command);

            command.Session.Connection.Send(message);
        }

        public void Initialize()
        {
            _subscription = new CompositeDisposable(
                _eventAggregator.GetEvent<IRequestSessionInfoMessage>().Subscribe(ProcessRequest),
                _eventAggregator.GetEvent<IRequestSessionMessage>().Subscribe(ProcessRequest),
                _eventAggregator.GetEvent<IRequestHarmonyConfigurationMessage>().Subscribe(ProcessRequest),
                _eventAggregator.GetEvent<IHarmonyCommandMessage>().Subscribe(ProcessCommand)
            );
        }

        public void Cleanup()
        {
            if (_subscription != null)
            {
                _subscription.Dispose();
                _subscription = null;
            }
        }
    }
}
