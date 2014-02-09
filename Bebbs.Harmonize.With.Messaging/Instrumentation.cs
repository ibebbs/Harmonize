using EventSourceProxy;

namespace Bebbs.Harmonize.With.Messaging
{
    public interface IMessages
    {
        void Received(Schema.Register message);

        void Received(Schema.Deregister message);

        void Received(Schema.Observe message);

        void Received(Schema.Subscribe message);

        void Received(Schema.Act message);

        void Sent(Schema.Register obj);

        void Sent(Schema.Deregister obj);

        void Sent(Schema.Observation obj);

        void Sent(Schema.Act obj);
    }

    public static class Instrumentation
    {
        public static readonly IMessages Messages = EventSourceImplementer.GetEventSourceAs<IMessages>();
    }
}
