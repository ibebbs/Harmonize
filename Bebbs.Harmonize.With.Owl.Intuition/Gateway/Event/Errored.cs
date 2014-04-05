using System;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.Event
{
    internal class Errored
    {
        public Errored(Exception exception)
        {
            Exception = exception;
        }

        public Exception Exception { get; private set; }
    }
}
