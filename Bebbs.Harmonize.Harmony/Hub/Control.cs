using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.Harmony.Hub
{
    internal class Control : With.Component.IControl
    {
        string With.Component.IControl.Name
        {
            get { return name; }
        }

        IEnumerable<With.Component.IAction> With.Component.IControl.Actions
        {
            get { return function; }
        }

        public string name { get; set; }

        public Action[] function { get; set; }
    }
}
