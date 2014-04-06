using Bebbs.Harmonize.With;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Sinks;
using System;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;

namespace Bebbs.Harmonize.Service
{
    public static class Instrumentation
    {
        private static readonly ObservableEventListener Listener = new ObservableEventListener();
        private static IDisposable Subscription;

        public static void Start(bool console, string logPath, string traceNames)
        {
            Listener.EnableEvents((EventSource)Harmonize.Instrumentation.Error, EventLevel.Warning, Keywords.All);
            Listener.EnableEvents((EventSource)Harmonize.Instrumentation.Harmonization, EventLevel.LogAlways, Keywords.All);

            traceNames.Split(',').Select(name => name.Trim()).ForEach(name => Listener.EnableEvents(name, EventLevel.LogAlways, Keywords.All));

            Subscription = console 
                ? (IDisposable)Listener.LogToConsole()
                : (IDisposable)Listener.LogToRollingFlatFile(logPath, 1048576, "yyyymmddhhMMss", RollFileExistsBehavior.Increment, RollInterval.Midnight, new EventTextFormatter(null, null, EventLevel.LogAlways)); 
        }

        public static void Stop()
        {
            if (Subscription != null)
            {
                Subscription.Dispose();
                Subscription = null;
            }
        }
    }
}
