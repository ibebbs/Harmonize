using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.LightwaveRf.Configuration
{
    public interface IDevices
    {

    }

    public class Devices : IDevices
    {
        public IEnumerable<Dimmer> Dimmers { get; set; }
    }
}
