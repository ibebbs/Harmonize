using Config = SimpleConfig.Configuration;

namespace Bebbs.Harmonize.Host.Configuration
{
    public interface IProvider
    {
        ISettings GetSettings();
    }

    internal class Provider : IProvider
    {
        public ISettings GetSettings()
        {
            return Config.Load<Settings>(sectionName: "hostContainer");
        }
    }
}
