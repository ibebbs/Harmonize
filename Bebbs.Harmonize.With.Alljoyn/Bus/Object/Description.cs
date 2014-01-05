using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Alljoyn.Bus.Object
{
    internal class Description
    {
        public Description(string path, IEnumerable<Facet> facets)
        {
            Path = path;
            Facets = facets;
        }

        public string Path { get; private set; }

        public IEnumerable<Facet> Facets { get; private set; }
    }
}
