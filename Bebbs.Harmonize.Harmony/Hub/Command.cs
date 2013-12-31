using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.Harmony.Hub
{
    internal class Command : With.Command.ICommand
    {
        public string command { get; set; }
        public string type { get; set; }
        public string deviceId { get; set; }
    }
}
