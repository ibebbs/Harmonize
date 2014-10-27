
using System.Xml.Serialization;
namespace Bebbs.Harmonize.With.LightwaveRf.Configuration
{
    public interface IDevice
    {
        string Name { get; }

        string Description { get; }

        string Location { get; }

        DeviceType Type { get; }

        byte RoomNumber { get; }

        byte DeviceNumber { get; }
    }

    [XmlInclude(typeof(Dimmer))]
    public class Device : IDevice
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public DeviceType Type { get; set; }

        public byte RoomNumber { get; set; }

        public byte DeviceNumber { get; set; }
    }
}
