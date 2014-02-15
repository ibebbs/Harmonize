using System.IO;
using System.Xml.Serialization;

namespace Bebbs.Harmonize.With.Messaging
{
    public interface ISerializer
    {
        string Serialize(Schema.Message message);
        Schema.Message Deserialize(string message);
    }

    internal class Serializer : ISerializer
    {
        private static readonly XmlSerializer Instance = new XmlSerializer(typeof(Schema.Message));

        public string Serialize(Schema.Message message)
        {
            using (StringWriter writer = new StringWriter())
            {
                Instance.Serialize(writer, message);

                return writer.ToString();
            }
        }

        public Schema.Message Deserialize(string message)
        {
            using (StringReader reader = new StringReader(message))
            {
                return (Schema.Message)Instance.Deserialize(reader);
            }
        }
    }
}
