using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.State
{
    public interface IMachine : IInitialize, ICleanup
    {

    }

    internal class Machine : IMachine
    {
        private readonly Gateway.Event.IMediator _eventMediator;
        private readonly IFactory _stateFactory;
        private readonly ITransition _transition;

        private IState _state = null;
        private IDisposable _subscription;

        public Machine(Gateway.Event.IMediator eventMediator, State.IFactory stateFactory, ITransition transition)
        {
            _eventMediator = eventMediator;
            _stateFactory = stateFactory;
            _transition = transition;
        }

        private void ConstructState(Context.IContext context)
        {
            _state = _stateFactory.For(context);
        }

        private void EnterState()
        {
            if (_state != null)
            {
                Instrumentation.State.Machine.EnteringState(_state.Name);

                _state.OnEnter();

                Instrumentation.State.Machine.EnteredState(_state.Name);
            }
        }

        private void ExitState()
        {
            if (_state != null)
            {
                State.Name name = _state.Name;

                Instrumentation.State.Machine.ExitingState(name);

                _state.OnExit();
                _state = null;

                Instrumentation.State.Machine.ExitedState(name);
            }
        }

        private void TransitionState(Event.Transition message)
        {
            ExitState();

            ConstructState(message.Context);

            EnterState();
        }

        public void Initialize()
        {
            _subscription = _eventMediator.GetEvent<Event.Transition>().Subscribe(TransitionState);

            _transition.ToDisconnected();
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
