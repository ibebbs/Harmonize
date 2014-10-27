using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.LightwaveRf.WifiLink
{
    internal class InstructionNumber
    {
        private uint _value;

        public string Next()
        {
            _value++;

            if (_value > 999) _value = 999;

            return _value.ToString("000");
        }
    }
}
