using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Component
{
    public interface IAction
    {
        With.Command.ICommand Command { get; }

        string Name { get; }
    }
}
