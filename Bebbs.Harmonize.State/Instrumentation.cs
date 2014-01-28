using EventSourceProxy;

namespace Bebbs.Harmonize.State
{
    [EventSourceImplementation(Name = "Bebbs-Harmonize-State-Store")]
    public interface IStoreInstrumentation
    {
        void ConnectingToEventStore(string ipAddress, int port);

        void DisconnectingFromEventStore(string ipAddress, int port);

        void Storing(With.Message.IObservation message);

        void Storing(With.Message.IStarted message);

        void Storing(With.Message.IStopped message);

        void Storing(With.Message.IRegisterDevice message);

        void Storing(With.Message.IDeregisterDevice message);
    }

    public static class Instrumentation
    {
        public static readonly IStoreInstrumentation Store = EventSourceImplementer.GetEventSourceAs<IStoreInstrumentation>();
    }
}
