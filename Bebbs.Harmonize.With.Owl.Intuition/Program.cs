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

                    configure.SetDescription("Owl Intuition integration with Harmonize");
                    configure.SetDisplayName("HarmonizeWithOwlIntuition");
                    configure.SetServiceName("HarmonizeWithOwlIntuition");
                }
            );
        }
    }
}
