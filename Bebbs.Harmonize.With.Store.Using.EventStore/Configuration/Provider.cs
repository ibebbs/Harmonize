using Config = SimpleConfig.Configuration;

namespace Bebbs.Harmonize.With.Store.Using.EventStore.Configuration
{
    public interface IProvider
    {
        ISettings GetSettings();
    }

    internal class Provider : IProvider
    {
        public ISettings GetSettings()
        {
            return Config.Load<Settings>(sectionName: "eventStore");
        }
    }
}
