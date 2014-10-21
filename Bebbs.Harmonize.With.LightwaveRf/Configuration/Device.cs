
namespace Bebbs.Harmonize.With.LightwaveRf.Configuration
{
    public interface IDevice
    {
        string Name { get; }

        string Description { get; }

        string Location { get; }

        DeviceType Type { get; }

        uint RoomNumber { get; }

        uint DeviceNumber { get; }
    }

    public class Device : IDevice
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public DeviceType Type { get; set; }

        public uint RoomNumber { get; set; }

        public uint DeviceNumber { get; set; }
    }
}
