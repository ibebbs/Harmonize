using Bebbs.Harmonize.With.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.LightwaveRf.Dimmer
{
    internal class Entity : ILightwaveEntity
    {
        private readonly Configuration.Dimmer _dimmer;
        private readonly WifiLink.IBridge _bridge;
        private readonly Messaging.Client.IEndpoint _clientEndpoint;

        private readonly Level _level;
        private readonly On _on;
        private readonly Off _off;

        private readonly Subject<With.Message.IMessage> _messages;

        private IDisposable _subscription;

        public Entity(Configuration.Dimmer dimmer, WifiLink.IBridge bridge, With.Messaging.Client.IEndpoint clientEndpoint)
        {
            _dimmer = dimmer;
            _bridge = bridge;
            _clientEndpoint = clientEndpoint;

            _on = new On(dimmer, bridge);
            _off = new Off(dimmer, bridge);
            _level = new Level(dimmer, bridge);

            _messages = new Subject<Message.IMessage>();

            Identity = _dimmer.ToEntityIdentity();
            Description = _dimmer.ToEntityDescription();
            Observables = Enumerable.Empty<IObservable>();
            Actionables = new IActionable[] { _off, _on, _level };
        }

        private IDisposable SubscribeOnAction()
        {
            return _messages.OfType<With.Message.IAction>().Where(action => IdentityComparer.Default.Equals(action.Actionable, _on.Identity)).Subscribe(_on.Execute);
        }

        private IDisposable SubscribeOffAction()
        {
            return _messages.OfType<With.Message.IAction>().Where(action => IdentityComparer.Default.Equals(action.Actionable, _off.Identity)).Subscribe(_off.Execute);
        }

        private IDisposable SubscribeLevelAction()
        {
            return _messages.OfType<With.Message.IAction>().Where(action => IdentityComparer.Default.Equals(action.Actionable, _level.Identity)).Subscribe(_level.Execute);
        }

        void IObserver<With.Message.IMessage>.OnCompleted()
        {
            _messages.OnCompleted();
        }

        void IObserver<With.Message.IMessage>.OnError(System.Exception error)
        {
            _messages.OnError(error);
        }

        void IObserver<With.Message.IMessage>.OnNext(Message.IMessage value)
        {
            _messages.OnNext(value);
        }

        public Task Initialize()
        {
            _subscription = new CompositeDisposable(
                SubscribeOnAction(),
                SubscribeOffAction(),
                SubscribeLevelAction()
            );

            _clientEndpoint.Register(LightwaveRf.Constants.Identity, this, this);

            return TaskEx.Done;
        }

        public Task CleanUp()
        {
            if (_subscription != null)
            {
                _subscription.Dispose();
                _subscription = null;
            }

            _clientEndpoint.Deregister(LightwaveRf.Constants.Identity, this);

            return TaskEx.Done;
        }

        public IIdentity Identity { get; private set; }

        public IEntityDescription Description { get; private set; }

        public IEnumerable<IObservable> Observables { get; private set; }

        public IEnumerable<IActionable> Actionables { get; private set; }
    }
}
