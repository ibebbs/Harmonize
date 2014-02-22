using System;

namespace Bebbs.Harmonize.With.Harmony.Messages
{
    public interface IErrorMessage : IMessage
    {
        Exception Exception { get; }
    }

    internal class ErrorMessage : IErrorMessage
    {
        public ErrorMessage(Exception exception)
        {
            Exception = exception;
        }

        public Exception Exception { get; private set; }
    }
}
