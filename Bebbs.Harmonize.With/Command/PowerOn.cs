
namespace Bebbs.Harmonize.With.Command
{
    public class PowerOn : ICommand
    {
        public PowerOn(string deviceId)
        {
            DeviceId = deviceId;
        }

        public string DeviceId { get; private set; }
    }
}
