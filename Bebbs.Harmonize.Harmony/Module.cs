using Bebbs.Harmonize.With.Harmony.Services;
using Ninject;

namespace Bebbs.Harmonize.With.Harmony
{
    public class Module : HarmonizedModule
    {
        public override void Load()
        {
            Bind<State.IFactory>().To<State.Factory>().InSingletonScope();
            Bind<Messages.IFactory>().To<Messages.Factory>().InSingletonScope();

            Bind<Hub.Configuration.Parser>().ToSelf();
            Bind<Hub.Configuration.IParser>().ToMethod(ctx => EventSourceProxy.TracingProxy.Create<Hub.Configuration.IParser>(ctx.Kernel.Get<Hub.Configuration.Parser>())).InSingletonScope();

            Bind<XmppService>().ToSelf();
            Bind<IXmppService>().ToMethod(ctx => EventSourceProxy.TracingProxy.Create<IXmppService>(ctx.Kernel.Get<XmppService>())).InSingletonScope();
            Bind<AuthenticationService>().ToSelf();
            Bind<IAuthenticationService, IInitialize, ICleanup>().ToMethod(ctx => EventSourceProxy.TracingProxy.Create<IAuthenticationService>(ctx.Kernel.Get<AuthenticationService>())).InSingletonScope();
            Bind<HarmonyService>().ToSelf();
            Bind<IHarmonyService, IInitialize, ICleanup>().ToMethod(ctx => EventSourceProxy.TracingProxy.Create<IHarmonyService>(ctx.Kernel.Get<HarmonyService>())).InSingletonScope();
            Bind<State.Machine>().ToSelf();
            Bind(new[] { typeof(State.IMachine), typeof(IInitialize), typeof(IStart), typeof(IStop), typeof(ICleanup) }).ToMethod(ctx => EventSourceProxy.TracingProxy.Create<State.IMachine>(ctx.Kernel.Get<State.Machine>())).InSingletonScope();
        }
    }
}
