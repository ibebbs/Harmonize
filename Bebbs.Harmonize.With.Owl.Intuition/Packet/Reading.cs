using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bebbs.Harmonize.With.Owl.Intuition.Packet
{
    [XmlRoot("reading")]
    public class Reading
    {
        public static string Wrap(string packet)
        {
            return string.Format("<reading>{0}</reading>", packet.Replace('\'', '"'));
        }

        [XmlElement("electricity")]
        public Electricity Electricity { get; set; }
    }
}
