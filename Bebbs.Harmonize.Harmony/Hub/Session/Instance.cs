using agsXMPP;

namespace Bebbs.Harmonize.With.Harmony.Hub.Session
{
    public interface IInstance
    {
        Endpoint.IInstance Endpoint { get; }
    }

    internal class Instance : IInstance
    {
        public Instance(Endpoint.IInstance endpoint)
        {
            Endpoint = endpoint;
        }

        public Endpoint.IInstance Endpoint { get; private set; }
    }
}
