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
        IEnumerable<IControl> Controls { get; }

        string Name { get; }
        string Type { get; }
        string Manufacturer { get; }
        string Model { get; }
    }
}
