
namespace Bebbs.Harmonize.With.Messaging
{
    public class Module : HarmonizedModule
    {
        public override void Load()
        {
            Bind<Mapping.IHelper>().To<Mapping.Helper>().InSingletonScope();
            Bind<Serialization.IHelper>().To<Serialization.Helper>().InSingletonScope();
            Bind<Serialization.IWrapper>().To<Serialization.Wrapper>().InSingletonScope();
            Bind<IHelper>().To<Helper>().InSingletonScope();
        }
    }
}
