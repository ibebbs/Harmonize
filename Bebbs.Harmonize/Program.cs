using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.HostConfigurators;
using Topshelf.ServiceConfigurators;

namespace Bebbs.Harmonize
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(
                configure =>
                {
                    configure.Service<Host.Container<Harmonizer>>(
                        service =>
                        {
                            service.ConstructUsing(name => new Host.Container<Harmonizer>());
                            service.WhenStarted(async harmonizer => await harmonizer.Start());
                            service.WhenStopped(async harmonizer => await harmonizer.Stop());
                        }
                    );
                    configure.RunAsLocalSystem();
                    configure.StartAutomaticallyDelayed();

                    configure.SetDescription("Harmonize Host");
                    configure.SetDisplayName("Harmonize Host");
                    configure.SetServiceName("HarmonizeHost");
                }
            );
        }
    }
}
