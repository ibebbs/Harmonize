using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Owl.Intuition.State
{
    public interface IMachine : IDisposable, IInitialize, IStart, IStop, ICleanup
    {

    }

    internal class Machine : IMachine
    {
        private readonly Event.IMediator _eventMediator;
        private readonly IFactory _stateFactory;
        private readonly ITransition _transition;

        private IState _state = null;
        private IDisposable _subscription;

        public Machine(Event.IMediator eventMediator, State.IFactory stateFactory, ITransition transition)
        {
            _eventMediator = eventMediator;
            _stateFactory = stateFactory;
            _transition = transition;
        }

        public void Dispose()
        {
            Cleanup();

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

        private void TransitionState(Event.TransitionState message)
        {
            ExitState();

            ConstructState(message.Context);

            EnterState();
        }

        public void Initialize()
        {
            _subscription = _eventMediator.GetEvent<Event.TransitionState>().Subscribe(TransitionState);

            _transition.ToDisconnected();
        }

        public Task Start()
        {
            IObservable<Unit> observable = ObservableExtentions.Either(
                _eventMediator.GetEvent<Event.Started>().Timeout(TimeSpan.FromSeconds(30)),
                _eventMediator.GetEvent<Event.Errored>(),
                (started, error) =>
                {
                    if (error != null)
                    {
                        throw new ApplicationException("Error starting Harmony", error.Exception);
                    }
                    else
                    {
                        return Unit.Default;
                    }
                }
            );

            Task result = observable.ToTask();

            _transition.ToConnecting();

            return Task.FromResult<object>(null);
        }

        public Task Stop()
        {
            return Task.FromResult<object>(null);
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
