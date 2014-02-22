using Bebbs.Harmonize.With.Owl.Intuition.Values;

namespace Bebbs.Harmonize.With.Owl.Intuition.State.Context
{
    public interface IRegistration : IContext
    {
        Command.Endpoint.IInstance CommandEndpoint { get; }
        Version Version { get; }
    }

    internal class Registration : IRegistration
    {
        public Registration(Command.Endpoint.IInstance commandEndpoint, Version version)
        {
            CommandEndpoint = commandEndpoint;
            Version = version;
        }

        public Command.Endpoint.IInstance CommandEndpoint { get; private set; }
        public Version Version { get; private set; }
    }
}
