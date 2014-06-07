using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(
                configure =>
                {
                    configure.Service<Host.Container<Bootstrapper>>(
                        service =>
                        {
                            service.ConstructUsing(name => new Host.Container<Bootstrapper>());
                            service.WhenStarted(async connector => await connector.Start());
                            service.WhenStopped(async connector => await connector.Stop());
                        }
                    );
                    configure.RunAsLocalSystem();
                    configure.StartAutomaticallyDelayed();

                    configure.SetDescription("Harmonize Messaging Bridge between RabbitMq and SignalR");
                    configure.SetDisplayName("HarmonizeWithMessagingViaSignalR");
                    configure.SetServiceName("HarmonizeWithMessagingViaSignalR");
                }
            );
        }
    }
}
