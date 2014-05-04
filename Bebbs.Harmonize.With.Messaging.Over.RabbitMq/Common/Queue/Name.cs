
namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq.Common.Queue
{
    public interface IName
    {
        string For(Component.IIdentity entity);
    }

    internal class Name : IName
    {
        public string For(Component.IIdentity entity)
        {
            return entity.Value;
        }
    }
}
