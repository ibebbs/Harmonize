using Bebbs.Harmonize.With;
using Bebbs.Harmonize.With.Harmony.Services;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reactive.Disposables;
using Cons = System.Console;

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
                ObservableEventListener Listener = new ObservableEventListener();
                Listener.EnableEvents((EventSource)Harmonize.Instrumentation.Error, EventLevel.LogAlways, Keywords.All);
                Listener.EnableEvents((EventSource)Harmonize.Instrumentation.Harmonization, EventLevel.LogAlways, Keywords.All);

                //ObservableEventListener harmonyEventListener = new ObservableEventListener();
                //harmonyEventListener.EnableEvents((EventSource)With.Harmony.Instrumentation.State, EventLevel.LogAlways, Keywords.All);

                //ObservableEventListener owlCommandEventListener = new ObservableEventListener();
                //owlCommandEventListener.EnableEvents((EventSource)With.Owl.Intuition.Instrumentation.Command.Endpoint, EventLevel.LogAlways, Keywords.All);

                //ObservableEventListener owlPacketEventListener = new ObservableEventListener();
                //owlPacketEventListener.EnableEvents((EventSource)With.Owl.Intuition.Instrumentation.Packet.Endpoint, EventLevel.LogAlways, Keywords.All);

                //ObservableEventListener owlStateEventListener = new ObservableEventListener();
                //owlStateEventListener.EnableEvents((EventSource)With.Owl.Intuition.Instrumentation.State.Machine, EventLevel.LogAlways, Keywords.All);

                //ObservableEventListener xmppEventListener = new ObservableEventListener();
                //xmppEventListener.EnableEvents((EventSource)With.Harmony.Instrumentation.Xmpp, EventLevel.LogAlways, Keywords.All);

                //ObservableEventListener alljoynEventListener = new ObservableEventListener();
                //alljoynEventListener.EnableEvents(With.Alljoyn.Instrumentation.Coordinator, EventLevel.LogAlways, Keywords.All);

                //ObservableEventListener storeEventListener = new ObservableEventListener();
                //storeEventListener.EnableEvents((EventSource)State.Instrumentation.Store, EventLevel.LogAlways, Keywords.All);

                //ObservableEventListener messagingEventListener = new ObservableEventListener();
                //messagingEventListener.EnableEvents((EventSource)With.Messaging.Instrumentation.Messages, EventLevel.LogAlways, Keywords.All);

                options.TraceEvents.Split(',').Select(name => Listener.EnableEvents(name, EventLevel.LogAlways, Keywords.All)).ForEach(Cons.WriteLine);

                using (new CompositeDisposable(Listener.LogToConsole()))
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
