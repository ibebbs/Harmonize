using System;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Service
{
    public interface IMapper
    {
        With.Component.IIdentity Map(string connectionId);
        With.Component.IIdentity Map(Common.Identity entity);
        With.Component.IEntity Map(Common.Entity entity);

        With.Message.IMessage Map(Common.Message message);
        Common.Message Map(With.Message.IMessage message);
    }

    internal class Mapper : IMapper
    {
        public With.Component.IIdentity Map(string connectionId)
        {
            return new With.Component.Identity(connectionId);
        }

        public With.Component.IEntity Map(Common.Entity entity)
        {
            return new With.Component.Entity(new With.Component.Identity(entity.Identity.Value), null, null, null);
        }

        public With.Component.IIdentity Map(Common.Identity entity)
        {
            return new With.Component.Identity(entity.Value);
        }

        public Message.IMessage Map(Common.Message message)
        {
            throw new NotImplementedException();
        }

        public Common.Message Map(Message.IMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
