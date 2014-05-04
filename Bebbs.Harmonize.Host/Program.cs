using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.HostConfigurators;
using Topshelf.ServiceConfigurators;

namespace Bebbs.Harmonize.Host
{
    class Program
    {
        private static Harmonizer ConstructHarmonizer(string name)
        {
            Configuration.Provider provider = new Configuration.Provider();
            Configuration.ISettings settings = provider.GetSettings();

            Options options = new Options(settings.ModulePatterns);

            return new Harmonizer(options);
        }

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
