using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.Harmony.Hub
{
    internal class Location : With.Component.ILocation
    {
        public Location(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
