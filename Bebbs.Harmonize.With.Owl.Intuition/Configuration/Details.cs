using System.Xml.Serialization;

namespace Bebbs.Harmonize.With.Owl.Intuition.Configuration
{
    [XmlRoot("Configuration")]
    public class Details
    {
        [XmlElement("Gateway", typeof(Device))]
        public Device[] Devices { get; set; }
    }
}
