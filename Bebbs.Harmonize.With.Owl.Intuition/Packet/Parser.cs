using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bebbs.Harmonize.With.Owl.Intuition.Packet
{
    public interface IParser
    {
        IEnumerable<IPacket> GetPackets(string packets);
    }

    internal class Parser : IParser
    {
        public IEnumerable<IPacket> GetPackets(string packets)
        {
            throw new NotImplementedException();
        }
    }
}
