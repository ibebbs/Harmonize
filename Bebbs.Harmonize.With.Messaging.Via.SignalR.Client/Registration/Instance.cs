using System;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Client.Registration
{
    public interface IInstance
    {
        string Key { get; }

        Common.Dto.Identity Client { get; }

        Common.Dto.Entity Entity { get; }

        IObserver<Message.IMessage> Consumer { get; }
    }

    internal class Instance : IInstance
    {
        public Instance(Common.Dto.Identity client, Common.Dto.Entity entity, IObserver<Message.IMessage> consumer)
        {
            Client = client;
            Entity = entity;
            Consumer = consumer;

            Key = Registration.Key.For(client, entity.Identity);
        }

        public string Key { get; private set; }

        public Common.Dto.Identity Client { get; private set; }

        public Common.Dto.Entity Entity { get; private set; }

        public IObserver<Message.IMessage> Consumer { get; private set; }
    }
}
