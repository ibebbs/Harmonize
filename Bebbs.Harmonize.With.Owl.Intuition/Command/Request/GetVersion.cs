using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Owl.Intuition.Command.Request
{
    public class GetVersion : IRequest
    {
        public string AsString()
        {
            return string.Format("{0},{1}", Verb.Get, Subject.Version);
        }
    }
}
