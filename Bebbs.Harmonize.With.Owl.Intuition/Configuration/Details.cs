using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bebbs.Harmonize.With.Owl.Intuition.Configuration
{
    [XmlRoot("Configuration")]
    public class Details
    {
        [XmlArray("Devices")]
        [XmlArrayItem("Device", typeof(Device))]
        public Device[] Devices { get; set; }
    }
}
