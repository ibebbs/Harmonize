using System.Collections.Generic;

namespace Bebbs.Harmonize.With.LightwaveRf.Configuration
{
    public class WifiLink
    {
        public Settings Settings { get; set; }

        public IEnumerable<Device> Devices { get; set; }
    }
}
