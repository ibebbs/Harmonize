using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Component
{
    public interface IDevice
    {
        IIdentity Identity { get; }
        IDescription Description { get; }
        ILocation Location { get; }
        IEnumerable<IControl> Controls { get; }
    }
}
