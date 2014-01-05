using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Alljoyn.Bus.Object
{
    internal class Facet
    {
        public Facet(string name, IEnumerable<MethodHandler> methodHandlers)
        {
            Name = name;
            MethodHandlers = methodHandlers;
        }

        public string Name { get; private set; }

        public IEnumerable<MethodHandler> MethodHandlers { get; private set; }
    }
}
