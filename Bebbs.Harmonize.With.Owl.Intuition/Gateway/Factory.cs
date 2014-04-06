using Ninject;
using Ninject.Extensions.ChildKernel;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway
{
    public interface IFactory
    {
        IContext CreateDeviceInContext(Settings.IProvider settingsProvider);
    }

    internal class Factory : IFactory
    {
        private readonly IKernel _kernel;

        public Factory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IContext CreateDeviceInContext(Settings.IProvider settingsProvider)
        {
            ChildKernel kernel = new ChildKernel(_kernel);

            kernel.Bind<Command.Response.IBuilder>().To<Command.Response.Builder.VersionResponse>().InSingletonScope();
            kernel.Bind<Command.Response.IBuilder>().To<Command.Response.Builder.RostaResponse>().InSingletonScope();
            kernel.Bind<Command.Response.IBuilder>().To<Command.Response.Builder.DeviceResponse>().InSingletonScope();
            kernel.Bind<Command.Response.IBuilder>().To<Command.Response.Builder.UdpResponse>().InSingletonScope();
            kernel.Bind<Command.Response.IBuilder>().To<Command.Response.Builder.SaveResponse>().InSingletonScope();
            kernel.Bind<Command.Response.IParser>().To<Command.Response.Parser>().InSingletonScope();
            kernel.Bind<Command.Endpoint.IFactory>().To<Command.Endpoint.Factory>().InSingletonScope();
            
            kernel.Bind<Packet.IParser>().To<Packet.Parser>().InSingletonScope();
            kernel.Bind<Packet.Endpoint.IFactory>().To<Packet.Endpoint.Factory>().InSingletonScope();

            kernel.Bind<Event.IMediator>().To<Event.Mediator>().InSingletonScope();
            kernel.Bind<State.Event.IFactory>().To<State.Event.Factory>().InSingletonScope();
            kernel.Bind<State.Context.IFactory>().To<State.Context.Factory>().InSingletonScope();
            
            kernel.Bind<State.ITransition>().To<State.Transition>().InSingletonScope();
            kernel.Bind<State.IFactory>().To<State.Factory>().InSingletonScope();
            kernel.Bind<State.IMachine>().To<State.Machine>().InSingletonScope();

            kernel.Bind<Entity.IFactory>().To<Entity.Factory>().InSingletonScope();

            kernel.Bind<Entity.Observable.IFactory>().To<Entity.Observable.CurrentElectricityConsumptionFactory>().InSingletonScope();
            kernel.Bind<Entity.Observable.IAbstractFactory>().To<Entity.Observable.AbstractFactory>().InSingletonScope();

            kernel.Bind<IBridge>().To<Bridge>().InSingletonScope();
            kernel.Bind<IInstance>().To<Instance>().InSingletonScope();
            kernel.Bind<IContext>().To<Context>().InSingletonScope();

            kernel.Bind<Settings.IProvider>().ToConstant(settingsProvider);

            return kernel.Get<IContext>();
        }
    }
}
