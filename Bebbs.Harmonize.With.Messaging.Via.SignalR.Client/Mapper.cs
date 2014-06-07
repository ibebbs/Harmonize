
namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Client
{
    public interface IMapper
    {
        Message.IMessage Map(Common.Message message);
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
    }
}
