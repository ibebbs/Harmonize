using System.Collections.Generic;
using System.Linq;

namespace Bebbs.Harmonize
{
    public interface IOptions
    {
        IEnumerable<IModulePattern> ModulePatterns { get; }
        IEnumerable<With.HarmonizedModule> Modules { get; }
    }

    public class Options : IOptions
    {
        public Options(IEnumerable<IModulePattern> modulePatterns, IEnumerable<With.HarmonizedModule> modules)
        {
            ModulePatterns = (modulePatterns ?? Enumerable.Empty<IModulePattern>()).ToArray();
            Modules = (modules ?? Enumerable.Empty<With.HarmonizedModule>()).ToArray();
        }

        public Options(IEnumerable<With.HarmonizedModule> modules) : this(null, modules) { }

        public Options(IEnumerable<IModulePattern> modulePatterns) : this(modulePatterns, null) { }

        public string ModulePath { get; private set; }

        public IEnumerable<IModulePattern> ModulePatterns { get; private set; }

        public IEnumerable<With.HarmonizedModule> Modules { get; private set; }
    }
}
