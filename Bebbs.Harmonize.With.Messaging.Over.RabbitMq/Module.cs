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
            Bind<Common.Connection.IFactory>().To<Common.Connection.Factory>().InSingletonScope();

            Bind<Client.Configuration.IProvider>().To<Client.Configuration.Provider>().InSingletonScope();
            Bind<Common.Configuration.ISettings, Client.Configuration.ISettings>().ToMethod(ctx => ctx.Kernel.Get<Client.Configuration.IProvider>().GetSettings()).InSingletonScope();
            Bind<Messaging.Client.IEndpoint>().To<Client.Endpoint>();

            Bind<Component.Configuration.IProvider>().To<Component.Configuration.Provider>().InSingletonScope();
            Bind<Common.Configuration.ISettings, Component.Configuration.ISettings>().ToMethod(ctx => ctx.Kernel.Get<Component.Configuration.IProvider>().GetSettings()).InSingletonScope();
            Bind<Messaging.Component.IEndpoint>().To<Component.Endpoint>();

            Bind<Service.Configuration.IProvider>().To<Service.Configuration.Provider>().InSingletonScope();
            Bind<Common.Configuration.ISettings, Service.Configuration.ISettings>().ToMethod(ctx => ctx.Kernel.Get<Service.Configuration.IProvider>().GetSettings()).InSingletonScope();
            Bind<Messaging.Service.IEndpoint>().To<Service.Endpoint>();
        }
    }
}
