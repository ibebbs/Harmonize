using System;

namespace Bebbs.Harmonize.With.Owl.Intuition.State.Context
{
    public interface IFault : IContext
    {
        Command.Endpoint.IInstance CommandEndpoint { get; }
        Exception Exception { get; }
    }

    internal class Fault : IFault
    {
        public Fault(Command.Endpoint.IInstance commandEndpoint, Exception exception)
        {
            CommandEndpoint = commandEndpoint;
            Exception = exception;
        }

        public Command.Endpoint.IInstance CommandEndpoint { get; private set; }
        public Exception Exception { get; private set; }
    }
}
