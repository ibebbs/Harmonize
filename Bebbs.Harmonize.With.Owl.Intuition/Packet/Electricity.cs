using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bebbs.Harmonize.With.Owl.Intuition.Packet
{
    [XmlRoot("electricity")]
    public class Electricity : IPacket
    {
        [XmlAttribute("id")]
        public long Id { get; set; }

        [XmlElement("signal", typeof(Signal))]
        public Signal Signal { get; set; }

        [XmlElement("battery", typeof(Battery))]
        public Battery Battery { get; set; }

        [XmlElement("chan", typeof(Channel))]
        public Channel[] Channels { get; set; }
    }
}
