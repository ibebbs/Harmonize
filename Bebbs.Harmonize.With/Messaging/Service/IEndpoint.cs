using System;

namespace Bebbs.Harmonize.With.Messaging.Service
{
    public interface IEndpoint : Common.IEndpoint
    {
        void Start(IObserver<Message.IMessage> consumer);

        void Stop();

        void Register(Component.IIdentity entity);

        void Deregister(Component.IIdentity entity);

        void AddObserver(Component.IIdentity sourceEntity, Component.IIdentity observable, Component.IIdentity targetEntity);

        void RemoveObserver(Component.IIdentity sourceEntity, Component.IIdentity observable, Component.IIdentity targetEntity);
    }
}
