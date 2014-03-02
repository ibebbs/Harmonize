
namespace Bebbs.Harmonize.With.Harmony.Hub.Endpoint
{
    public interface IFactory
    {
        IInstance Create(string server, Session.IInfo sessionInfo);
    }

    internal class Factory : IFactory
    {
        public IInstance Create(string server, Session.IInfo sessionInfo)
        {
            return new Instance(server, sessionInfo);
        }
    }
}
