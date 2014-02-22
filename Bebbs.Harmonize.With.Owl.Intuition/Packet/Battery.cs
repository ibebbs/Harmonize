using System.Xml.Serialization;

namespace Bebbs.Harmonize.With.Owl.Intuition.Packet
{
    [XmlRoot("battery")]
    public class Battery
    {
        [XmlAttribute("level")]
        public string Level { get; set; }
    }
}
