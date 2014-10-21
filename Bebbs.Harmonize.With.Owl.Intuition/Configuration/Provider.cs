using Config = SimpleConfig.Configuration;

namespace Bebbs.Harmonize.With.Owl.Intuition.Configuration
{
    public interface IProvider
    {
        ISettings GetSettings();
    }

    internal class Provider : IProvider
    {
        public ISettings GetSettings()
        {
            return Config.Load<Settings>(sectionName: "owlIntuition");
        }
    }
}
