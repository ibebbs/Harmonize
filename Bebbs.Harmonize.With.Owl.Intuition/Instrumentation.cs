using EventSourceProxy;
using System;
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
        [Event(1, Message = "CreatedAt", Level = EventLevel.Informational)]
        void CreatedAt(string address, int port);

        [Event(2, Message = "Response", Level = EventLevel.Informational)]
        void Response(Command.IResponse response);

        [Event(3, Message = "Request", Level = EventLevel.Informational)]
        void Request(Command.IRequest request);

        [Event(4, Message = "Error", Level = EventLevel.Error)]
        void Error(System.Exception exception);
    }

    [EventSourceImplementation(Name = "Bebbs-Harmonize-With-Owl-Intuition-Packet-Endpoint")]
    public interface IPacketEndpoint : IEndpoint
    {
        [Event(1, Message = "Reading", Level = EventLevel.Informational)]
        void Reading(Packet.IReading packet);
    }

    [EventSourceImplementation(Name = "Bebbs-Harmonize-With-Owl-Intuition-Packet-Parser")]
    public interface IPacketParser
    {
        [Event(1, Message = "Error", Level = EventLevel.Error)]
        void Error(System.Exception e);
    }

    [EventSourceImplementation(Name = "Bebbs-Harmonize-With-Owl-Intuition-State-Machine")]
    public interface IMachine
    {
        [Event(1, Message = "EnteringState", Level = EventLevel.Informational)]
        void EnteringState(Gateway.State.Name name);

        [Event(2, Message = "EnteredState", Level = EventLevel.Informational)]
        void EnteredState(Gateway.State.Name name);

        [Event(3, Message = "ExitingState", Level = EventLevel.Informational)]
        void ExitingState(Gateway.State.Name name);

        [Event(4, Message = "ExitedState", Level = EventLevel.Informational)]
        void ExitedState(Gateway.State.Name name);
    }

    public static class Instrumentation
    {
        static Instrumentation()
        {
            TraceParameterProvider.Default
                .ForAnything()
                    .With<Exception>()
                        .Trace(ex => ex.Message).As("Exception")
                        .Trace(ex => ex.StackTrace).As("CallStack");
        }

        public static class Command
        {
            private static readonly Lazy<ICommandEndpoint> LazyEndpoint = new Lazy<ICommandEndpoint>(() => EventSourceImplementer.GetEventSourceAs<ICommandEndpoint>());
            
            public static ICommandEndpoint Endpoint
            {
                get { return LazyEndpoint.Value; }
            }
        }

        public static class Packet
        {
            private static readonly Lazy<IPacketEndpoint> LazyEndpoint = new Lazy<IPacketEndpoint>(() => EventSourceImplementer.GetEventSourceAs<IPacketEndpoint>());

            private static readonly Lazy<IPacketParser> LazyParser = new Lazy<IPacketParser>(() => EventSourceImplementer.GetEventSourceAs<IPacketParser>());

            public static IPacketEndpoint Endpoint
            {
                get { return LazyEndpoint.Value; }
            }

            public static IPacketParser Parser
            {
                get { return LazyParser.Value; }
            }
        }

        public static class State
        {
            private static readonly Lazy<IMachine> LazyMachine = new Lazy<IMachine>(() => EventSourceImplementer.GetEventSourceAs<IMachine>());

            public static IMachine Machine
            {
                get { return LazyMachine.Value; }
            }
        }

        private static readonly Lazy<IConfiguration> LazyConfiguration = new Lazy<IConfiguration>(() => EventSourceImplementer.GetEventSourceAs<IConfiguration>());

        public static IConfiguration Configuration
        {
            get { return LazyConfiguration.Value;  }
        }
    }
}
