using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace Bebbs.Harmonize.With.Owl.Intuition.Gateway.Event
{
    public class Connected
    {
        public Connected(string name, string remarks, PhysicalAddress macAddress, IEnumerable<Command.Response.Device> roster)
        {
            Name = name;
            Remarks = remarks;
            MacAddress = macAddress;
            Roster = (roster ?? Enumerable.Empty<Command.Response.Device>()).ToArray();
        }

        public string Name { get; private set; }
        public string Remarks { get; private set; }
        public PhysicalAddress MacAddress { get; private set; }
        public IEnumerable<Command.Response.Device> Roster { get; private set; }
    }
}
