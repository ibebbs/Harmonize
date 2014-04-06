using EventSourceProxy;

namespace Bebbs.Harmonize.With.Owl.Intuition
{
    public interface IConfiguration
    {
        void Error(string error);

        void Failure(string failure);
    }

    public interface IEndpoint
    {
        void Send(string value);
        void Receive(string value);
    }

    public interface ICommandEndpoint : IEndpoint
    {
        void Response(Command.IResponse response);

        void Request(Command.IRequest request);

        void Error(System.Exception exception);
    }

    public interface IPacketEndpoint : IEndpoint
    {
        void Reading(Packet.IReading packet);
    }

    public interface IPacketParser
    {
        void Error(System.Exception e);
    }

    public interface IMachine
    {
        void EnteringState(Gateway.State.Name name);
        void EnteredState(Gateway.State.Name name);
        void ExitingState(Gateway.State.Name name);
        void ExitedState(Gateway.State.Name name);
    }

    public static class Instrumentation
    {
        public static class Command
        {
            public static readonly ICommandEndpoint Endpoint = EventSourceImplementer.GetEventSourceAs<ICommandEndpoint>();
        }

        public static class Packet
        {
            public static readonly IPacketEndpoint Endpoint = EventSourceImplementer.GetEventSourceAs<IPacketEndpoint>();

            public static readonly IPacketParser Parser = EventSourceImplementer.GetEventSourceAs<IPacketParser>();
        }

        public static class State
        {
            public static readonly IMachine Machine = EventSourceImplementer.GetEventSourceAs<IMachine>();
        }

        public static readonly IConfiguration Configuration = EventSourceImplementer.GetEventSourceAs<IConfiguration>();
    }
}
