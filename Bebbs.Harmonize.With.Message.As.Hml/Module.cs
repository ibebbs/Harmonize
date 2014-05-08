using Ninject.Modules;

namespace Bebbs.Harmonize.With.Message.As.Hml
{
    public class Module : NinjectModule
    {
        public override void Load()
        {
            Bind<Schema.IMapper>().To<Schema.Mapper>().InSingletonScope();
            Bind<With.Message.ISerializer>().To<Serializer>().InSingletonScope();
        }
    }
}
