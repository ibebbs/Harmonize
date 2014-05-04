using Bebbs.Harmonize.With.Serialization;
using System.IO;
using System.Reflection;
using Config = SimpleConfig.Configuration;

namespace Bebbs.Harmonize.Host.Configuration
{
    public interface IProvider
    {
        ISettings GetSettings();
    }

    internal class Provider : IProvider
    {
        private static readonly XmlSerializer<Settings> Serializer = new XmlSerializer<Settings>();

        public ISettings GetSettings()
        {
            return Config.Load<Settings>(sectionName: "hostContainer");
        }
    }
}
