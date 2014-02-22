using System.IO;
using System.Xml.Serialization;

namespace Bebbs.Harmonize.With.Serialization
{
    public class XmlSerializer<T>
    {
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(T));

        public string Serialize(T value)
        {
            using (StringWriter writer = new StringWriter())
            {
                Serializer.Serialize(writer, value);

                return writer.ToString();
            }
        }
 
        public T Deserialize(string xml)
        {
            using (StringReader reader = new StringReader(xml))
            {
                return (T)Serializer.Deserialize(reader);
            }
        }
    }
}
