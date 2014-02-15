
namespace Bebbs.Harmonize.With.Messaging.Over.RabbitMq
{
    public class Module : HarmonizedModule
    {
        public override void Load()
        {
            Bind<IEndpoint, IMessageEndpoint>().To<Endpoint>().InSingletonScope();
        }
    }
}
