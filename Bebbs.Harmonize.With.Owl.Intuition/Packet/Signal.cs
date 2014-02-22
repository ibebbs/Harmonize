using System.Xml.Serialization;

namespace Bebbs.Harmonize.With.Owl.Intuition.Packet
{
    [XmlRoot("signal")]
    public class Signal
    {
        [XmlAttribute("rssi")]
        public int SignalStrength { get; set; }

        [XmlAttribute("lqi")]
        public int LinkQuality { get; set; }
    }
}
