using Bebbs.Harmonize.With.Messaging.Via.SignalR.Common;
using System;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Client.Registration
{
    public interface IFactory
    {
        IInstance For(With.Component.IIdentity registrar, With.Component.IEntity entity, IObserver<With.Message.IMessage> consumer);
    }

    internal class Factory : IFactory
    {
        public IInstance For(With.Component.IIdentity registrar, With.Component.IEntity entity, IObserver<With.Message.IMessage> consumer)
        {
            return new Instance(registrar.AsDto(), entity.AsDto(), consumer);
        }
    }
}
