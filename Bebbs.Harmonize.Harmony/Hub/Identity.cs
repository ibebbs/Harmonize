using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.Harmony.Hub
{
    public class Identity : With.Component.IIdentity
    {
        public Identity(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
