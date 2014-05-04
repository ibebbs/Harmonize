using System.Collections.Generic;
using System.Xml.Serialization;

namespace Bebbs.Harmonize.With.Owl.Intuition.Configuration
{
    public interface ISettings
    {
        IEnumerable<IDevice> Devices { get; }
    }

    public class Settings : ISettings
    {
        IEnumerable<IDevice> ISettings.Devices
        {
            get { return Devices; }
        }

        public IEnumerable<Device> Devices { get; set; }
    }
}
