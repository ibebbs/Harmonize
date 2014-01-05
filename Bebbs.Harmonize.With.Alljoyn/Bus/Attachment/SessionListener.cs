using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Alljoyn.Bus.Attachment
{
    internal class SessionListener : AllJoynUnity.AllJoyn.SessionPortListener
    {
        protected override bool AcceptSessionJoiner(ushort sessionPort, string joiner, AllJoynUnity.AllJoyn.SessionOpts opts)
        {
            return true;
        }
    }
}
