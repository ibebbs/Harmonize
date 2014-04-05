using Bebbs.Harmonize.With.Serialization;
using System.IO;
using System.Reflection;

namespace Bebbs.Harmonize.With.Owl.Intuition.Configuration
{
    public interface IProvider
    {
        Details GetConfiguration();
    }

    internal class Provider : IProvider
    {
        private static readonly XmlSerializer<Details> Serializer = new XmlSerializer<Details>();

        public Details GetConfiguration()
        {
            Assembly current = Assembly.GetAssembly(typeof(Module));

            string configurationFile = Path.ChangeExtension(current.Location, "xml");

            using (FileStream stream = File.OpenRead(configurationFile))
            {
                return Serializer.Deserialize(stream);
            }
        }
    }
}
