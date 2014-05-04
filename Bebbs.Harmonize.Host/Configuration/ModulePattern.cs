using System.Xml.Serialization;

namespace Bebbs.Harmonize.Host.Configuration
{
    public interface IModulePattern
    {
        string Path { get; }
        
        string Pattern { get; }
    }

    [XmlType("modulePattern")]
    public class ModulePattern : IModulePattern
    {
        public string Path { get; set; }

        public string Pattern { get; set; }
    }
}
