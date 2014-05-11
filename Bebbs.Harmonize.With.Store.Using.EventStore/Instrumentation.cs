using EventSourceProxy;
using System;
using System.Net;

namespace Bebbs.Harmonize.With.Store.Using.EventStore
{
    [EventSourceImplementation(Name = "Bebbs-Harmonize-With-Store-Using-EventStore")]
    public interface IStoreInstrumentation
    {
        void ConnectingToEventStore(string ipAddress, int port);

        void ConnectedToEventStore(IPAddress iPAddress, int port);

        void DisconnectedFromEventStore(IPAddress iPAddress, int port);

        void EventStoreErrorOccured(Exception e);

        void ReconnectingToEventStore();

        void EventStoreAuthenticationFailed(string message);

        void DisconnectingFromEventStore(string ipAddress, int port);

        void Storing(With.Message.IRegister message);

        void Storing(With.Message.IDeregister message);

        void Storing(With.Message.IObservation message);

        void Storing(With.Message.IAction message);
    }

    public static class Instrumentation
    {
        public static readonly IStoreInstrumentation Store = EventSourceImplementer.GetEventSourceAs<IStoreInstrumentation>();
    }
}
