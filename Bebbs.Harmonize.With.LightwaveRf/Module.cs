using EventSourceProxy;
using Ninject;
using Ninject.Modules;

namespace Bebbs.Harmonize.With.LightwaveRf
{
    public class Module : NinjectModule
    {
        public override void Load()
        {
            Bind<Configuration.Provider>().ToSelf(); // "Bebbs-Harmonize-With-LightwaveRf-Configuration-Provider"
            Bind<Configuration.IProvider>().ToMethod(ctx => TracingProxy.CreateWithActivityScope<Configuration.IProvider>(ctx.Kernel.Get<Configuration.Provider>())).InSingletonScope();

            Bind<Dimmer.Builder>().ToSelf(); // "Bebbs-Harmonize-With-LightwaveRf-Dimmer-Builder"
            Bind<Entity.IBuilder, Dimmer.IBuilder>().ToMethod(ctx => TracingProxy.CreateWithActivityScope<Dimmer.IBuilder>(ctx.Kernel.Get<Dimmer.Builder>())).InSingletonScope();

            Bind<Entity.Factory>().ToSelf(); // "Bebbs-Harmonize-With-LightwaveRf-Entity-Factory"
            Bind<Entity.IFactory>().ToMethod(ctx => TracingProxy.CreateWithActivityScope<Entity.IFactory>(ctx.Kernel.Get<Entity.Factory>())).InSingletonScope();

            Bind<WifiLink.CommandBuilder>().ToSelf(); // "Bebbs-Harmonize-With-LightwaveRf-WifiLink-CommandBuilder"
            Bind<WifiLink.ICommandBuilder>().ToMethod(ctx => TracingProxy.CreateWithActivityScope<WifiLink.ICommandBuilder>(ctx.Kernel.Get<WifiLink.CommandBuilder>())).InSingletonScope();
            Bind<WifiLink.CommandEndpoint>().ToSelf(); // "Bebbs-Harmonize-With-LightwaveRf-WifiLink-CommandEndpoint"
            Bind<WifiLink.ICommandEndpoint>().ToMethod(ctx => TracingProxy.CreateWithActivityScope<WifiLink.ICommandEndpoint>(ctx.Kernel.Get<WifiLink.CommandEndpoint>()));
            Bind<WifiLink.QueryEndpoint>().ToSelf(); // "Bebbs-Harmonize-With-LightwaveRf-WifiLink-QueryEndpoint"
            Bind<WifiLink.IQueryEndpoint>().ToMethod(ctx => TracingProxy.CreateWithActivityScope<WifiLink.IQueryEndpoint>(ctx.Kernel.Get<WifiLink.QueryEndpoint>()));
            Bind<WifiLink.Bridge>().ToSelf(); // "Bebbs-Harmonize-With-LightwaveRf-WifiLink-Bridge"
            Bind<WifiLink.IBridge>().ToMethod(ctx => TracingProxy.CreateWithActivityScope<WifiLink.IBridge>(ctx.Kernel.Get<WifiLink.Bridge>())).InSingletonScope();

            Bind<Service>().ToSelf(); // "Bebbs-Harmonize-With-LightwaveRf-Service"
            Bind<IService>().ToMethod(ctx => TracingProxy.CreateWithActivityScope<IService>(ctx.Kernel.Get<Service>()));
        }
    }
}
