using Bebbs.Harmonize.With.Harmony.Services;
using Ninject;

namespace Bebbs.Harmonize.With.Harmony
{
    public class Module : HarmonizedModule
    {
        public override void Load()
        {
            Bind<Settings.IProvider>().To<Settings.Provider>().InSingletonScope();
            Bind<State.IFactory>().To<State.Factory>().InSingletonScope();

            Bind<Messages.IFactory>().To<Messages.Factory>().InSingletonScope();
            Bind<Messages.IMediator>().To<Messages.Mediator>().InSingletonScope();

            Bind<Hub.Authentication.IProvider>().To<Hub.Authentication.Provider>().InSingletonScope();
            Bind<Hub.Endpoint.IFactory>().To<Hub.Endpoint.Factory>().InSingletonScope();

            Bind<AuthenticationService>().ToSelf();
            Bind<IAuthenticationService, IInitialize, ICleanup>().ToMethod(ctx => EventSourceProxy.TracingProxy.Create<IAuthenticationService>(ctx.Kernel.Get<AuthenticationService>())).InSingletonScope();

            Bind<State.Machine>().ToSelf();
            Bind(new[] { typeof(State.IMachine), typeof(IInitialize), typeof(IStart), typeof(IStop), typeof(ICleanup) }).ToMethod(ctx => EventSourceProxy.TracingProxy.Create<State.IMachine>(ctx.Kernel.Get<State.Machine>())).InSingletonScope();
        }
    }
}
