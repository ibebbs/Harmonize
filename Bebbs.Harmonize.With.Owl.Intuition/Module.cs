
namespace Bebbs.Harmonize.With.Owl.Intuition
{
    public class Module : HarmonizedModule
    {
        public override void Load()
        {
            Kernel.Bind<Settings.IProvider>().To<Settings.Provider>().InSingletonScope();

            Kernel.Bind<Command.Response.IBuilder>().To<Command.Response.Builder.VersionResponse>().InSingletonScope();
            Kernel.Bind<Command.Response.IBuilder>().To<Command.Response.Builder.RostaResponse>().InSingletonScope();
            Kernel.Bind<Command.Response.IBuilder>().To<Command.Response.Builder.DeviceResponse>().InSingletonScope();
            Kernel.Bind<Command.Response.IBuilder>().To<Command.Response.Builder.UdpResponse>().InSingletonScope();
            Kernel.Bind<Command.Response.IBuilder>().To<Command.Response.Builder.SaveResponse>().InSingletonScope();
            Kernel.Bind<Command.Response.IParser>().To<Command.Response.Parser>().InSingletonScope();
            Kernel.Bind<Command.Endpoint.IFactory>().To<Command.Endpoint.Factory>().InSingletonScope();

            Kernel.Bind<Packet.IParser>().To<Packet.Parser>().InSingletonScope();
            Kernel.Bind<Packet.Endpoint.IFactory>().To<Packet.Endpoint.Factory>().InSingletonScope();

            Kernel.Bind<State.Event.IMediator>().To<State.Event.Mediator>().InSingletonScope();
            Kernel.Bind<State.Event.IFactory>().To<State.Event.Factory>().InSingletonScope();

            Kernel.Bind<State.Context.IFactory>().To<State.Context.Factory>().InSingletonScope();

            Kernel.Bind<State.ITransition>().To<State.Transition>().InSingletonScope();
            Kernel.Bind<State.IFactory>().To<State.Factory>().InSingletonScope();
            Kernel.Bind(new[] { typeof(State.IMachine), typeof(IInitialize), typeof(IStart), typeof(IStop), typeof(ICleanup) }).To<State.Machine>().InSingletonScope();
        }
    }
}
