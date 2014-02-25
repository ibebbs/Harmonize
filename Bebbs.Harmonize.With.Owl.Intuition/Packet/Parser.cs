using Bebbs.Harmonize.With.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Bebbs.Harmonize.With.Owl.Intuition.Packet
{
    public interface IParser
    {
        IEnumerable<IPacket> GetPackets(string packets);
    }

    internal class Parser : IParser
    {
        public static readonly XmlSerializer<Wrapper> Serializer = new XmlSerializer<Wrapper>();

        private Wrapper GetWrapper(string packets)
        {
            try
            {
                string wrapped = Wrapper.Wrap(packets);

                Wrapper wrapper = Serializer.Deserialize(wrapped);

                return wrapper;
            }
            catch (Exception e)
            {
                Instrumentation.Packet.Parser.Error(e);

                return new Wrapper();
            }
        }

        public IEnumerable<IPacket> GetPackets(string packets)
        {
            Wrapper wrapper = GetWrapper(packets);

            if (wrapper.Electricity != null)
            {
                yield return wrapper.Electricity;
            }
        }
    }
}
