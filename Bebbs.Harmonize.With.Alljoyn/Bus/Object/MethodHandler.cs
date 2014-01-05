using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Alljoyn.Bus.Object
{
    internal class MethodHandler
    {
        public MethodHandler(string name, Action<AllJoynUnity.AllJoyn.InterfaceDescription.Member, AllJoynUnity.AllJoyn.Message> action)
        {
            Name = name;
            Action = action;
        }

        public string Name { get; private set; }

        public Action<AllJoynUnity.AllJoyn.InterfaceDescription.Member, AllJoynUnity.AllJoyn.Message> Action { get; private set; }
    }
}
