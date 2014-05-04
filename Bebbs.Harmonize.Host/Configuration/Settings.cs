using System.Collections.Generic;
using System.Xml.Serialization;

namespace Bebbs.Harmonize.Host.Configuration
{
    public interface ISettings
    {
        IEnumerable<IModulePattern> ModulePatterns { get; }
    }

    public class Settings : ISettings
    {
        IEnumerable<IModulePattern> ISettings.ModulePatterns 
        {
            get { return ModulePatterns; }
        }

        public IEnumerable<ModulePattern> ModulePatterns { get; set; }
    }
}
