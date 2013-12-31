using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Component
{
    public interface IControl
    {
        string Name { get; }

        IEnumerable<IAction> Actions { get; }
    }
}
