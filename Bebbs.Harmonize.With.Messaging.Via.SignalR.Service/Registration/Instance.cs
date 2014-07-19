using System;
using System.Reactive.Subjects;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Service.Registration
{
    public interface IInstance
    {
        string Key { get; }
        With.Component.IIdentity Registrar { get; }
        With.Component.IEntity Entity { get; }
        IObserver<With.Message.IMessage> Consumer { get; }
    }

    internal class Instance : IInstance
    {
        private readonly Subject<Message.IMessage> _consumer;
        private IDisposable _subscription;

        public Instance(string connectionId, With.Component.IIdentity registrar, With.Component.IEntity entity, Action<With.Message.IMessage> processor)
        {
            ConnectionId = connectionId;
            Registrar = registrar;
            Entity = entity;

            Key = Registration.Key.For(connectionId, registrar, entity.Identity);

            _consumer = new Subject<With.Message.IMessage>();
            _subscription = _consumer.Subscribe(processor);
        }

        public string Key { get; private set; }

        public string ConnectionId { get; private set; }

        public With.Component.IIdentity Registrar { get; private set; }

        public With.Component.IEntity Entity { get; private set; }

        public IObserver<Message.IMessage> Consumer 
        {
            get { return _consumer; }
        }
    }
}
