using System;

namespace Bebbs.Harmonize.With.Messaging.Service
{
    public interface IEndpoint : Common.IEndpoint
    {
        void Start(IObserver<Message.IMessage> consumer);

        void Stop();

        void Add(With.Component.IIdentity component);

        void Remove(With.Component.IIdentity component);

        void Register(With.Component.IIdentity entity);

        void Deregister(With.Component.IIdentity entity);

        void AddObserver(With.Component.IIdentity sourceEntity, With.Component.IIdentity observable, With.Component.IIdentity targetEntity);

        void RemoveObserver(With.Component.IIdentity sourceEntity, With.Component.IIdentity observable, With.Component.IIdentity targetEntity);
    }
}
