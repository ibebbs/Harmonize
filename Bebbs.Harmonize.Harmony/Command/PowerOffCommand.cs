
namespace Bebbs.Harmonize.Harmony.Command
{
    public class PowerOffCommand : ICommand
    {
        public PowerOffCommand(string deviceId)
        {
            DeviceId = deviceId;
        }

        public string DeviceId { get; private set; }
    }
}
