using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.Service
{
    internal class ModulePattern : IModulePattern
    {
        public string Path { get; set; }

        public string Pattern { get; set; }
    }
}
