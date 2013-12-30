using Bebbs.Harmonize.With;
using System;

namespace Bebbs.Harmonize.Harmony.State
{
    public interface IFactory
    {
        IState ConstructState(Name state);
    }

    internal class Factory : IFactory
    {
        private readonly IGlobalEventAggregator _eventAggregator;
        private readonly IAsyncHelper _asyncHelper;
        private readonly Command.IFactory _commandFactory;

        public Factory(IGlobalEventAggregator eventAggregator, IAsyncHelper asyncHelper, Command.IFactory harmonyCommandFactory)
        {
            _eventAggregator = eventAggregator;
            _asyncHelper = asyncHelper;
            _commandFactory = harmonyCommandFactory;
        }

        public IState ConstructState(Name state)
        {
            switch(state)
            {
                case Name.Authenticating: return new Authenticating(_eventAggregator, _asyncHelper);
                case Name.RetrievingSessionInfo: return new RetrievingSessionInfo(_eventAggregator, _asyncHelper);
                case Name.EstablishingSession: return new EstablishingSession(_eventAggregator, _asyncHelper);
                case Name.Synchronizing: return new Synchronizing(_eventAggregator, _asyncHelper);
                case Name.Started: return new Started(_eventAggregator, _asyncHelper, _commandFactory);
                case Name.Starting: return new Starting(_eventAggregator, _asyncHelper);
                case Name.Stopped: return new Stopped(_eventAggregator, _asyncHelper);
                case Name.Stopping: return new Stopping(_eventAggregator, _asyncHelper);
                case Name.Faulted: return new Faulted(_eventAggregator, _asyncHelper);
                default: throw new ArgumentException("Unknown state", "state");
            }
        }
    }
}
