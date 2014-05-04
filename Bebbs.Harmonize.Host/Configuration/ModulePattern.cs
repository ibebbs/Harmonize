using System.Xml.Serialization;

namespace Bebbs.Harmonize.Host.Configuration
{
    [XmlType("modulePattern")]
    public class ModulePattern : IModulePattern
    {
        public string Path { get; set; }

        public string Pattern { get; set; }
    }
}
