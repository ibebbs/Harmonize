
namespace Bebbs.Harmonize.With.Messaging
{
    public class Module : HarmonizedModule
    {
        public override void Load()
        {
            Bind<IMapping>().To<Mapping>().InSingletonScope();
            Bind<IWrapper>().To<Wrapper>().InSingletonScope();

            Bind<IBridge, IInitialize, ICleanup>().To<Bridge>().InSingletonScope();
        }
    }
}
