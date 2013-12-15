using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using System.Diagnostics.Tracing;
using System.Reactive.Disposables;

namespace Bebbs.Harmonize.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ObservableEventListener harmonyEventListener = new ObservableEventListener();
            harmonyEventListener.EnableEvents((EventSource) Harmony.EventSource.Log, EventLevel.LogAlways, Keywords.All);

            ObservableEventListener xmppEventListener = new ObservableEventListener();
            xmppEventListener.EnableEvents((EventSource)Harmony.Services.XmppEventSource.Log, EventLevel.LogAlways, Keywords.All);

            using (new CompositeDisposable(harmonyEventListener.LogToConsole(), xmppEventListener.LogToConsole()))
            {
                Client client = new Client();

                client.Start();

                System.Console.WriteLine("Started");
                System.Console.WriteLine("Hit Return to stop");
                System.Console.ReadLine();

                client.Stop();
            }
        }
    }
}
