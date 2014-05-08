using Bebbs.Harmonize.With.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Message.As.Hml
{
    internal class Serializer : ISerializer
    {
        private static readonly XmlSerializer<Schema.Message> XmlSerializer = new XmlSerializer<Schema.Message>();

        private readonly Schema.IMapper _mapper;

        public Serializer(Schema.IMapper mapper)
        {
            _mapper = mapper;
        }

        public string Serialize(IMessage message)
        {
            Schema.Message schema = _mapper.ToSchema(message);

            return XmlSerializer.Serialize(schema);
        }

        public IMessage Deserialize(string message)
        {
            Schema.Message schema = XmlSerializer.Deserialize(message);

            return _mapper.ToMessage(schema);
        }
    }
}
