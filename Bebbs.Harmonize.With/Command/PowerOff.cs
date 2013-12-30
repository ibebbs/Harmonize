
namespace Bebbs.Harmonize.With.Command
{
    public class PowerOff : ICommand
    {
        public PowerOff(string deviceId)
        {
            DeviceId = deviceId;
        }

        public string DeviceId { get; private set; }
    }
}
