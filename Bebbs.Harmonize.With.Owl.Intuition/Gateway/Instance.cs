using Ninject;
using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway
{
    public interface IInstance : IInitialize, ICleanup, IStart, IStop
    {

    }

    internal class Instance : IInstance
    {
        private readonly Event.IMediator _eventMediator;
        private readonly Entity.IFactory _entityFactory;
        private readonly State.IMachine _stateMachine;

        private IDisposable _eventSubscription;

        private Entity.IInstance _entity;

        public Instance(Event.IMediator eventMediator, State.IMachine stateMachine, Entity.IFactory entityFactory)
        {
            _eventMediator = eventMediator;
            _stateMachine = stateMachine;
            _entityFactory = entityFactory;
        }

        private void Process(Event.Connected connection)
        {
            _entity = _entityFactory.Create(connection.Name, connection.Remarks, connection.MacAddress, connection.Roster);

            _eventMediator.Publish(new Event.Register(_entity));

            _entity.Initialize();
        }

        private void Process(Event.Disconnected disconnection)
        {
            if (_entity != null)
            {
                _entity.Cleanup();

                _eventMediator.Publish(new Event.Deregister(_entity));

                _entity = null;
            }
        }

        public void Initialize()
        {
            _eventSubscription = new CompositeDisposable(
                _eventMediator.GetEvent<Event.Connected>().Subscribe(Process),
                _eventMediator.GetEvent<Event.Disconnected>().Subscribe(Process)
            );

            _stateMachine.Initialize();
        }

        public void Cleanup()
        {
            _stateMachine.Cleanup();
            
            if (_eventSubscription != null)
            {
                _eventSubscription.Dispose();
                _eventSubscription = null;
            }
        }

        public Task Start()
        {
            IObservable<Unit> observable = ObservableExtentions.Either(
                _eventMediator.GetEvent<Gateway.Event.Started>().Timeout(TimeSpan.FromSeconds(30)),
                _eventMediator.GetEvent<Gateway.Event.Errored>(),
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

            _eventMediator.Publish(new Event.Connect());

            return Task.FromResult<object>(null);
        }

        public Task Stop()
        {
            return Task.FromResult<object>(null);
        }
    }
}
