using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Owl.Intuition.State
{
    public interface IMachine : IDisposable
    {

    }

    internal class Machine : IMachine
    {
        private readonly Event.IMediator _eventMediator;
        private readonly IFactory _stateFactory;

        private IState _state = null;
        private IDisposable _subscription;

        public Machine(Event.IMediator eventMediator, State.IFactory stateFactory, ITransition transition)
        {
            _eventMediator = eventMediator;
            _stateFactory = stateFactory;

            _subscription = _eventMediator.GetEvent<Event.TransitionState>().Subscribe(TransitionState);
            
            transition.ToDisconnected();
        }

        public void Dispose()
        {
            if (_subscription != null)
            {
                _subscription.Dispose();
                _subscription = null;
            }

            ExitState();
        }

        private void ConstructState(Context.IContext context)
        {
            _state = _stateFactory.For(context);
        }

        private void EnterState()
        {
            if (_state != null)
            {
                _state.OnEnter();
            }
        }

        private void ExitState()
        {
            if (_state != null)
            {
                _state.OnExit();
                _state = null;
            }
        }

        private void TransitionState(Event.TransitionState message)
        {
            ExitState();

            ConstructState(message.Context);

            EnterState();
        }
    }
}
