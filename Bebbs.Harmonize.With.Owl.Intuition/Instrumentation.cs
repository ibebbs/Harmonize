using EventSourceProxy;
using System.Diagnostics.Tracing;

namespace Bebbs.Harmonize.With.Owl.Intuition
{
    [EventSourceImplementation(Name = "Bebbs-Harmonize-With-Owl-Intuition-Configuration")]
    public interface IConfiguration
    {
        [Event(1, Message = "Error", Level = EventLevel.Error)]
        void Error(string error);

        [Event(2, Message = "Failure", Level = EventLevel.Warning)]
        void Failure(string failure);
    }

    public interface IEndpoint
    {
        void Send(string value);
        void Receive(string value);
    }

    [EventSourceImplementation(Name = "Bebbs-Harmonize-With-Owl-Intuition-Command-Endpoint")]
    public interface ICommandEndpoint : IEndpoint
    {
        void Response(Command.IResponse response);

        void Request(Command.IRequest request);

        void Error(System.Exception exception);
    }

    [EventSourceImplementation(Name = "Bebbs-Harmonize-With-Owl-Intuition-Packet-Endpoint")]
    public interface IPacketEndpoint : IEndpoint
    {
        void Reading(Packet.IReading packet);
    }

    [EventSourceImplementation(Name = "Bebbs-Harmonize-With-Owl-Intuition-Packet-Parser")]
    public interface IPacketParser
    {
        void Error(System.Exception e);
    }

    [EventSourceImplementation(Name = "Bebbs-Harmonize-With-Owl-Intuition-State-Machine")]
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
