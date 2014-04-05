using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.State.Component
{
    public class Entity
    {
        public Identity Identity { get; set; }
        public EntityDescription Description { get; set; }
        public IEnumerable<Observable> Observables { get; set; }
        public IEnumerable<Actionable> Actionables { get; set; }
    }
}
