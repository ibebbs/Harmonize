
namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Client
{
    public interface IMapper
    {
        Message.IMessage Map(Common.Message message);

        Common.Identity Map(With.Component.IIdentity entity);
    }

    internal class Mapper : IMapper
    {
        private class Message : With.Message.IMessage
        {

        }

        public With.Message.IMessage Map(Common.Message message)
        {
            return new Message();
        }

        public Common.Identity Map(With.Component.IIdentity identity)
        {
            return new Common.Identity { Value = identity.Value };
        }
    }
}
