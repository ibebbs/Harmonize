using EventSourceProxy;
using System;

namespace Bebbs.Harmonize
{
    public interface IError
    {
        void WhenStarting(With.IStart startable, Exception exception);

        void WhenStopping(With.IStop stoppable, Exception exception);
    }

    public static class Instrumentation
    {
        public static readonly IError Error = EventSourceImplementer.GetEventSourceAs<IError>();
    }
}
