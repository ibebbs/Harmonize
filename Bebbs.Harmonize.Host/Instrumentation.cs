using EventSourceProxy;
using System;
using System.Diagnostics.Tracing;

namespace Bebbs.Harmonize.Host
{
    [EventSourceImplementation(Name = "Bebbs-Harmonize-Host-Error")]
    public interface IError
    {
        [Event(1, Message = "Starting", Level = EventLevel.Error)]
        void Starting(string type, Exception exception);

        [Event(2, Message = "Stopping", Level = EventLevel.Error)]
        void Stopping(string type, Exception exception);

        [Event(3, Message = "LoadingModules", Level = EventLevel.Error)]
        void LoadingModules(string filePattern, Exception exception);

        [Event(4, Message = "Initializing", Level = EventLevel.Error)]
        void Initializing(string type, Exception exception);
    }

    [EventSourceImplementation(Name = "Bebbs-Harmonize-Host-Container")]
    public interface IContainer
    {
        [Event(1, Message = "Starting", Level = EventLevel.LogAlways)]
        void Starting();

        [Event(2, Message = "LoadingModules", Level = EventLevel.LogAlways)]
        void LoadingModules(string filePattern);

        [Event(3, Message = "Stopping", Level = EventLevel.LogAlways)]
        void Stopping();
    }

    public static class Instrumentation
    {
        static Instrumentation()
        {
            TraceParameterProvider.Default
                .ForAnything()
                    .With<Exception>()
                        .Trace(exception => exception.GetType().Name).As("ExceptionName")
                        .Trace(exception => exception.Message).As("ExceptionMessage")
                        .Trace(exception => exception.StackTrace == null ? string.Empty : exception.StackTrace.ToString()).As("ExceptionStack")
                        .Trace(exception => exception.InnerException).As("ExceptionInner");
        }

        private static readonly Lazy<IError> InternalError = new Lazy<IError>(() => EventSourceImplementer.GetEventSourceAs<IError>());

        private static readonly Lazy<IContainer> InternalContainer = new Lazy<IContainer>(() => EventSourceImplementer.GetEventSourceAs<IContainer>());
        
        public static IError Error
        {
            get { return InternalError.Value; }
        }

        public static IContainer Container
        {
            get { return InternalContainer.Value; }
        }
    }
}
