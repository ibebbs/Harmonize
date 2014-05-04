using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Bebbs.Harmonize.With.Owl.Intuition
{
    class Program
    {

        static void Main(string[] args)
        {
            HostFactory.Run(
                configure =>
                {
                    configure.Service<Harmonizer>(
                        service =>
                        {
                            service.ConstructUsing(ConstructHarmonizer);
                            service.WhenStarted(harmonizer => harmonizer.Start());
                            service.WhenStopped(harmonizer => harmonizer.Stop());
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
