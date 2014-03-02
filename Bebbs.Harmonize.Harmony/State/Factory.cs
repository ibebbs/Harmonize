using System;

namespace Bebbs.Harmonize.With.Harmony.State
{
    public interface IFactory
    {
        IState ConstructState(Name state);
    }

    internal class Factory : IFactory
    {
        private readonly Settings.IProvider _settingsProvider;
        private readonly Hub.Authentication.IProvider _hubAuthenticationProvider;
        private readonly Hub.Endpoint.IFactory _endpointFactory;
        private readonly Messages.IFactory _messageFactory;
        private readonly Messages.IMediator _messageMediator;
        private readonly IAsyncHelper _asyncHelper;

        public Factory(Settings.IProvider settingsProvider, Hub.Authentication.IProvider hubAuthenticationProvider, Hub.Endpoint.IFactory endpointFactory, Messages.IFactory messageFactory, Messages.IMediator messageMediator, IAsyncHelper asyncHelper)
        {
            _settingsProvider = settingsProvider;
            _hubAuthenticationProvider = hubAuthenticationProvider;
            _endpointFactory = endpointFactory;
            _messageFactory = messageFactory;
            _messageMediator = messageMediator;
            _asyncHelper = asyncHelper;
        }

        public IState ConstructState(Name state)
        {
            switch(state)
            {
                case Name.Authenticating: return new Authenticating(_messageMediator, _asyncHelper);
                case Name.RetrievingSessionInfo: return new RetrievingSessionInfo(_settingsProvider, _hubAuthenticationProvider, _messageMediator, _asyncHelper);
                case Name.EstablishingSession: return new EstablishingSession(_settingsProvider, _endpointFactory, _messageMediator, _asyncHelper);
                case Name.Synchronizing: return new Synchronizing(_messageMediator, _asyncHelper);
                case Name.Registration: return new Registration(_messageMediator, _asyncHelper);
                case Name.Started: return new Started(_messageMediator, _asyncHelper, _messageFactory);
                case Name.Deregistration: return new Deregistration(_messageMediator, _asyncHelper);
                case Name.Starting: return new Starting(_messageMediator, _asyncHelper);
                case Name.Stopped: return new Stopped(_messageMediator, _asyncHelper);
                case Name.Stopping: return new Stopping(_messageMediator, _asyncHelper);
                case Name.Faulted: return new Faulted(_messageMediator, _asyncHelper);
                default: throw new ArgumentException("Unknown state", "state");
            }
        }
    }
}
