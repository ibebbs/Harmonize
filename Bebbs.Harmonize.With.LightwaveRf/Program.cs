using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Bebbs.Harmonize.With.LightwaveRf
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

                    configure.SetDescription("LightwaveRf integration with Harmonize");
                    configure.SetDisplayName("HarmonizeWithLightwaveRf");
                    configure.SetServiceName("HarmonizeWithLightwaveRf");
                }
            );
        }
        /*
        static void Main(string[] args)
        {
            var config = SimpleConfig.Configuration.Load<Configuration.WifiLink>(sectionName: "wifiLink");

            Debug.Assert(config != null);
        }
        */
    }
}
