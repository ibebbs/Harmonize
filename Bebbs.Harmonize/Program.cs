﻿using Topshelf;

namespace Bebbs.Harmonize
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(
                configure =>
                {
                    configure.Service<Host.Container<IHarmonizer>>(
                        service =>
                        {
                            service.ConstructUsing(name => new Host.Container<IHarmonizer>(new Module()));
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
