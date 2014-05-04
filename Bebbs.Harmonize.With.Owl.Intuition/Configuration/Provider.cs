using Bebbs.Harmonize.With.Serialization;
using System.IO;
using System.Reflection;
using Config = SimpleConfig.Configuration;

namespace Bebbs.Harmonize.With.Owl.Intuition.Configuration
{
    public interface IProvider
    {
        Settings GetConfiguration();
    }

    internal class Provider : IProvider
    {
        private static readonly XmlSerializer<Settings> Serializer = new XmlSerializer<Settings>();

        public Settings GetConfiguration()
        {
            return Config.Load<Settings>(sectionName: "owlIntuition");
        }
    }
}
