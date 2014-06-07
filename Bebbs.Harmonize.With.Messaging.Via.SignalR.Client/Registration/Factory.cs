using System;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Client.Registration
{
    public interface IFactory
    {
        IInstance For(With.Component.IIdentity registrar, With.Component.IEntity entity, IObserver<With.Message.IMessage> consumer);
    }

    internal class Factory
    {
        public IInstance For(With.Component.IIdentity registrar, With.Component.IEntity entity, IObserver<With.Message.IMessage> consumer)
        {
            return new Instance(new Common.Identity { Value = registrar.Value }, new Common.Entity { Identity = new Common.Identity { Value = entity.Identity.Value } }, consumer);
        }
    }
}
