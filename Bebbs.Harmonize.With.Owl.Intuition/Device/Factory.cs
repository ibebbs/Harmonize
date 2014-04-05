using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Owl.Intuition.Device
{
    public interface IFactory
    {
        IContext CreateDeviceInContext(Settings.IProvider settingsProvider);
    }

    internal class Factory : IFactory
    {
        public IContext CreateDeviceInContext(Settings.IProvider settingsProvider)
        {
            StandardKernel kernel = new StandardKernel();

            kernel.Bind<Command.Response.IBuilder>().To<Command.Response.Builder.VersionResponse>().InSingletonScope();
            kernel.Bind<Command.Response.IBuilder>().To<Command.Response.Builder.RostaResponse>().InSingletonScope();
            kernel.Bind<Command.Response.IBuilder>().To<Command.Response.Builder.DeviceResponse>().InSingletonScope();
            kernel.Bind<Command.Response.IBuilder>().To<Command.Response.Builder.UdpResponse>().InSingletonScope();
            kernel.Bind<Command.Response.IBuilder>().To<Command.Response.Builder.SaveResponse>().InSingletonScope();
            kernel.Bind<Command.Response.IParser>().To<Command.Response.Parser>().InSingletonScope();
            kernel.Bind<Command.Endpoint.IFactory>().To<Command.Endpoint.Factory>().InSingletonScope();
            
            kernel.Bind<Packet.IParser>().To<Packet.Parser>().InSingletonScope();
            kernel.Bind<Packet.Endpoint.IFactory>().To<Packet.Endpoint.Factory>().InSingletonScope();
            
            kernel.Bind<State.Event.IMediator>().To<State.Event.Mediator>().InSingletonScope();
            kernel.Bind<State.Event.IFactory>().To<State.Event.Factory>().InSingletonScope();
            
            kernel.Bind<State.Context.IFactory>().To<State.Context.Factory>().InSingletonScope();
            
            kernel.Bind<State.ITransition>().To<State.Transition>().InSingletonScope();
            kernel.Bind<State.IFactory>().To<State.Factory>().InSingletonScope();
            kernel.Bind<State.IMachine>().To<State.Machine>().InSingletonScope();

            kernel.Bind<IInstance>().To<Instance>().InSingletonScope();
            kernel.Bind<IContext>().To<Context>().InSingletonScope();

            kernel.Bind<Settings.IProvider>().ToConstant(settingsProvider);

            return kernel.Get<IContext>();
        }
    }
}
