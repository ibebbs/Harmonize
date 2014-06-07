using System;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Client.Registration
{
    public interface IInstance
    {
        string Key { get; }

        Common.Identity Registrar { get; }

        Common.Entity Entity { get; }

        IObserver<Message.IMessage> Consumer { get; }
    }

    internal class Instance : IInstance
    {
        public Instance(Common.Identity registrar, Common.Entity entity, IObserver<Message.IMessage> consumer)
        {
            Registrar = registrar;
            Entity = entity;
            Consumer = consumer;

            Key = Registration.Key.For(registrar, entity.Identity);
        }

        public string Key { get; private set; }

        public Common.Identity Registrar { get; private set; }

        public Common.Entity Entity { get; private set; }

        public IObserver<Message.IMessage> Consumer { get; private set; }
    }
}
