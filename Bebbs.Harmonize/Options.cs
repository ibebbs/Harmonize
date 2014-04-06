using System.Collections.Generic;
using System.Linq;

namespace Bebbs.Harmonize
{
    public interface IOptions
    {
        IEnumerable<string> ModulePatterns { get; }
        IEnumerable<With.HarmonizedModule> Modules { get; }
    }

    public class Options : IOptions
    {
        public Options(IEnumerable<string> modulePattern, IEnumerable<With.HarmonizedModule> modules)
        {
            ModulePatterns = (modulePattern ?? Enumerable.Empty<string>()).ToArray();
            Modules = (modules ?? Enumerable.Empty<With.HarmonizedModule>()).ToArray();
        }

        public Options(IEnumerable<With.HarmonizedModule> modules) : this(null, modules) { }

        public Options(IEnumerable<string> modulePattern) : this(modulePattern, null) { }

        public IEnumerable<string> ModulePatterns { get; private set; }

        public IEnumerable<With.HarmonizedModule> Modules { get; private set; }
    }
}
