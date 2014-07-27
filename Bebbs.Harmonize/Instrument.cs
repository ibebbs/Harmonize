using EventSourceProxy;
using System;
using System.Diagnostics.Tracing;

namespace Bebbs.Harmonize
{
    [EventSourceImplementation(Name = "Bebbs-Harmonize-Action")]
    public interface IAction
    {
        [Event(1, Message = "Register", Level = EventLevel.Informational)]
        void Register(With.Message.IRegister message);

        [Event(2, Message = "Deregister", Level = EventLevel.Informational)]
        void Deregister(With.Message.IDeregister message);

        [Event(3, Message = "Add", Level = EventLevel.Informational)]
        void Add(With.Message.IAdd message);

        [Event(4, Message = "Remove", Level = EventLevel.Informational)]
        void Remove(With.Message.IRemove message);

        [Event(5, Message = "Observe", Level = EventLevel.Informational)]
        void Observe(With.Message.IObserve message);

        [Event(6, Message = "Ignore", Level = EventLevel.Informational)]
        void Ignore(With.Message.IIgnore message);
    }

    public static class Instrument
    {
        public static class Harmonize
        {
            private static readonly Lazy<IAction> _action = new Lazy<IAction>(() => EventSourceImplementer.GetEventSourceAs<IAction>());

            public static IAction Action 
            {
                get { return _action.Value; }
            }
        }

    }
}
