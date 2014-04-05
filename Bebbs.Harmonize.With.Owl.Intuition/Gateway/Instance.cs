using Ninject;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway
{
    public interface IInstance : IInitialize, ICleanup, IStart, IStop, IDisposable
    {

    }

    internal class Instance : IInstance
    {
        private IKernel _kernel;
        private readonly State.IMachine _stateMachine;
        private State.ITransition _stateTransition;
        private State.Event.IMediator _stateEventMediator;

        public Instance(IKernel kernel, State.IMachine stateMachine, State.ITransition stateTransition, State.Event.IMediator stateEventMediator)
        {
            _kernel = kernel;
            _stateMachine = stateMachine;
            _stateTransition = stateTransition;
            _stateEventMediator = stateEventMediator;
        }

        public void Dispose()
        {
            if (_kernel != null)
            {
                Cleanup();

                _kernel.Dispose();
            }
        }

        public void Initialize()
        {
            _stateMachine.Initialize();
        }

        public void Cleanup()
        {
            _stateMachine.Cleanup();
        }

        public Task Start()
        {
            IObservable<Unit> observable = ObservableExtentions.Either(
                _stateEventMediator.GetEvent<State.Event.Started>().Timeout(TimeSpan.FromSeconds(30)),
                _stateEventMediator.GetEvent<State.Event.Errored>(),
                (started, error) =>
                {
                    if (error != null)
                    {
                        throw new ApplicationException("Error starting Owl", error.Exception);
                    }
                    else
                    {
                        return Unit.Default;
                    }
                }
            );

            Task result = observable.ToTask();

            _stateTransition.ToConnecting();

            return Task.FromResult<object>(null);
        }

        public Task Stop()
        {
            return Task.FromResult<object>(null);
        }
    }
}
