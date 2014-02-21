using Bebbs.Harmonize.With.Harmony.Services;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using System.Diagnostics.Tracing;
using System.Reactive.Disposables;

namespace Bebbs.Harmonize.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Options options = new Options();

            CommandLine.Parser.Default.ParseArguments(args, options);

            if (!options.ShowingHelp)
            {
                ObservableEventListener harmonizeEventListener = new ObservableEventListener();
                harmonizeEventListener.EnableEvents((EventSource)Harmonize.Instrumentation.Error, EventLevel.LogAlways, Keywords.All);

                ObservableEventListener harmonyEventListener = new ObservableEventListener();
                harmonyEventListener.EnableEvents((EventSource)With.Harmony.EventSource.Log, EventLevel.LogAlways, Keywords.All);

                ObservableEventListener xmppEventListener = new ObservableEventListener();
                xmppEventListener.EnableEvents((EventSource)XmppEventSource.Log, EventLevel.LogAlways, Keywords.All);

                ObservableEventListener alljoynEventListener = new ObservableEventListener();
                alljoynEventListener.EnableEvents(With.Alljoyn.Instrumentation.Coordinator, EventLevel.LogAlways, Keywords.All);

                ObservableEventListener storeEventListener = new ObservableEventListener();
                storeEventListener.EnableEvents((EventSource)State.Instrumentation.Store, EventLevel.LogAlways, Keywords.All);

                ObservableEventListener messagingEventListener = new ObservableEventListener();
                messagingEventListener.EnableEvents((EventSource)With.Messaging.Instrumentation.Messages, EventLevel.LogAlways, Keywords.All);

                using (new CompositeDisposable(harmonizeEventListener.LogToConsole(), harmonyEventListener.LogToConsole(), xmppEventListener.LogToConsole(), alljoynEventListener.LogToConsole(), storeEventListener.LogToConsole(), messagingEventListener.LogToConsole()))
                {
                    Client client = new Client(options);

                    client.Start();

                    System.Console.WriteLine("Started");
                    System.Console.WriteLine("Hit Return to stop");
                    System.Console.ReadLine();

                    client.Stop();
                }
            }
        }
    }
}
