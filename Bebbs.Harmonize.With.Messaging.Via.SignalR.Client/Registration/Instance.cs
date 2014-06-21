using System;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Client.Registration
{
    public interface IInstance
    {
        string Key { get; }

        Common.Identity Client { get; }

        Common.Entity Entity { get; }

        IObserver<Message.IMessage> Consumer { get; }
    }

    internal class Instance : IInstance
    {
        public Instance(Common.Identity client, Common.Entity entity, IObserver<Message.IMessage> consumer)
        {
            Client = client;
            Entity = entity;
            Consumer = consumer;

            Key = Registration.Key.For(client, entity.Identity);
        }

        public string Key { get; private set; }

        public Common.Identity Client { get; private set; }

        public Common.Entity Entity { get; private set; }

        public IObserver<Message.IMessage> Consumer { get; private set; }
    }
}
