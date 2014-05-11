using Topshelf;

namespace Bebbs.Harmonize.With.Store.Using.EventStore
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(
                configure =>
                {
                    configure.Service<Host.Container<Connector>>(
                        service =>
                        {
                            service.ConstructUsing(name => new Host.Container<Connector>(new Module()));
                            service.WhenStarted(async connector => await connector.Start());
                            service.WhenStopped(async connector => await connector.Stop());
                        }
                    );
                    configure.RunAsLocalSystem();
                    configure.StartAutomaticallyDelayed();

                    configure.SetDescription("Harmonize With Store Using EventStore");
                    configure.SetDisplayName("Harmonize With Store Using EventStore");
                    configure.SetServiceName("HarmonizeWithStoreUsingEventStore");
                }
            );
        }
    }
}
