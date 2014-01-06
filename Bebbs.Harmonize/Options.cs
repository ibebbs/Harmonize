using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize
{
    public interface IOptions
    {
        IEnumerable<With.HarmonizedModule> Modules { get; }
    }

    public class Options : IOptions
    {
        public Options(IEnumerable<With.HarmonizedModule> modules)
        {
            Modules = modules;
        }

        public IEnumerable<With.HarmonizedModule> Modules { get; private set; }
    }
}
