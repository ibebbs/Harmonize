using System;
using Bebbs.Harmonize.With.Messaging.Via.SignalR.Common;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Service.Registration
{
    public interface IFactory
    {
        IInstance For(string connectionId, Common.Dto.Identity registrar, Common.Dto.Entity entity, Action<string, Common.Dto.Identity, Common.Dto.Identity, Common.Dto.Message> processor);
    }

    internal class Factory : IFactory
    {
        public IInstance For(string connectionId, Common.Dto.Identity registrar, Common.Dto.Entity entity, Action<string, Common.Dto.Identity, Common.Dto.Identity, Common.Dto.Message> processor)
        {
            return new Instance(connectionId, registrar.AsComponent(), entity.AsComponent(), message => processor(connectionId, registrar, entity.Identity, message.AsDynamicDto()));
        }
    }
}
