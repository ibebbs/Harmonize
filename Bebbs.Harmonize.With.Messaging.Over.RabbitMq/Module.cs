using Ninject;

namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq
{
    public class Module : HarmonizedModule
    {
        public override void Load()
        {
            Bind<Common.IConnectionFactory>().To<Common.ConnectionFactory>().InSingletonScope();
            Bind<Common.Routing.IKey>().To<Common.Routing.Key>().InSingletonScope();
            Bind<Common.Queue.IName>().To<Common.Queue.Name>().InSingletonScope();

            Bind<Service.Configuration.IProvider>().To<Service.Configuration.Provider>().InSingletonScope();
            Bind<Common.Configuration.ISettings, Service.Configuration.ISettings>().ToMethod(ctx => ctx.Kernel.Get<Service.Configuration.IProvider>().GetSettings()).InSingletonScope();
            Bind<Service.Connection.IFactory>().To<Service.Connection.Factory>().InSingletonScope();

            Bind<Messaging.Service.IEndpoint>().To<Service.Endpoint>();
        }
    }
}
