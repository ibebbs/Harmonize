using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bebbs.Harmonize.With.Owl.Intuition.Packet
{
    [XmlRoot("wrapper")]
    public class Wrapper
    {
        public static string Wrap(string packet)
        {
            return string.Format("<wrapper>{0}</wrapper>", packet.Replace('\'', '"'));
        }

        [XmlElement("electricity")]
        public Electricity Electricity { get; set; }
    }
}
