
namespace Bebbs.Harmonize.With.Messaging
{
    public class Module : HarmonizedModule
    {
        public override void Load()
        {
            Bind<Mapping.IHelper>().To<Mapping.Helper>().InSingletonScope();
            Bind<ISerializer>().To<Serializer>().InSingletonScope();
            Bind<IWrapper>().To<Wrapper>().InSingletonScope();

            Bind<IBridge, IInitialize, ICleanup>().To<Bridge>().InSingletonScope();
        }
    }
}
