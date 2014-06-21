using System;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Service.Registration
{
    public interface IFactory
    {
        IInstance For(string connectionId, Common.Entity entity, Action<string, Common.Identity, Common.Message> processor);
    }

    internal class Factory : IFactory
    {
        private readonly IMapper _mapper;

        public Factory(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IInstance For(string connectionId, Common.Entity entity, Action<string, Common.Identity, Common.Message> processor)
        {
            return new Instance(_mapper.Map(connectionId), _mapper.Map(entity), message => processor(connectionId, entity.Identity, _mapper.Map(message)));
        }
    }
}
