using Ninject;
using Ninject.Modules;

namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq
{
    public class Module : NinjectModule
    {
        public override void Load()
        {
            Bind<Common.ConnectionFactory>().ToSelf();
            Bind<Common.IConnectionFactory>().ToMethod(ctx => EventSourceProxy.TracingProxy.CreateWithActivityScope<Common.IConnectionFactory>(ctx.Kernel.Get<Common.ConnectionFactory>())).InSingletonScope();

            Bind<Common.Routing.IKey>().To<Common.Routing.Key>().InSingletonScope();
            Bind<Common.Queue.IName>().To<Common.Queue.Name>().InSingletonScope();

            Bind<Common.Connection.Factory>().ToSelf();
            Bind<Common.Connection.IFactory>().ToMethod(ctx => EventSourceProxy.TracingProxy.CreateWithActivityScope<Common.Connection.IFactory>(ctx.Kernel.Get<Common.Connection.Factory>())).InSingletonScope();

            Bind<Client.Configuration.IProvider>().To<Client.Configuration.Provider>().InSingletonScope();
            Bind<Common.Configuration.ISettings, Client.Configuration.ISettings>().ToMethod(ctx => ctx.Kernel.Get<Client.Configuration.IProvider>().GetSettings()).InSingletonScope();
            Bind<Client.Endpoint>().ToSelf();
            Bind<Messaging.Client.IEndpoint>().ToMethod(ctx => EventSourceProxy.TracingProxy.CreateWithActivityScope<Client.IEndpoint>(ctx.Kernel.Get<Client.Endpoint>()));

            Bind<Component.Configuration.IProvider>().To<Component.Configuration.Provider>().InSingletonScope();
            Bind<Common.Configuration.ISettings, Component.Configuration.ISettings>().ToMethod(ctx => ctx.Kernel.Get<Component.Configuration.IProvider>().GetSettings()).InSingletonScope();
            Bind<Messaging.Component.IEndpoint>().To<Component.Endpoint>();

            Bind<Service.Configuration.IProvider>().To<Service.Configuration.Provider>().InSingletonScope();
            Bind<Common.Configuration.ISettings, Service.Configuration.ISettings>().ToMethod(ctx => ctx.Kernel.Get<Service.Configuration.IProvider>().GetSettings()).InSingletonScope();
            Bind<Service.Endpoint>().ToSelf();
            Bind<Messaging.Service.IEndpoint>().ToMethod(ctx => EventSourceProxy.TracingProxy.CreateWithActivityScope<Service.IEndpoint>(ctx.Kernel.Get<Service.Endpoint>()));
        }
    }
}
