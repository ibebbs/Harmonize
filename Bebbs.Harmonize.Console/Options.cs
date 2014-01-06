using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.Console
{
    public class Options
    {
        [Option("Alljoyn", DefaultValue = true, HelpText = "Harmonize with Alljoyn", Required = false)]
        public bool UseAllJoyn { get; set; }
    }
}
